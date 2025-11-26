using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private void UpdateLastUpdatedTime() {
            LastUpdatedText.Text = "最終更新: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm");
        }

        private Dictionary<string, (double Latitude, double Longitude)> CityCoordinates;

        private void LoadCitiesFromJson() {
            string json = File.ReadAllText("cities.json");
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var prefectures = JsonSerializer.Deserialize<Dictionary<string, List<City>>>(json, options);

            CityCoordinates = new Dictionary<string, (double, double)>();

            // ComboBoxに渡すリストを作成
            var comboItems = new List<object>();

            foreach (var kv in prefectures) // kv.Key = 都道府県名, kv.Value = 都市リスト
            {
                // 都道府県見出し（選択不可）
                comboItems.Add(new ComboBoxItem {
                    Content = kv.Key,
                    IsEnabled = false,
                });

                //区切り線を追加
                comboItems.Add(new Separator());

                // 都市リスト（選択可）
                foreach (var city in kv.Value) {
                    CityCoordinates[city.name] = (city.lat, city.lon);
                    comboItems.Add(city.name);
                }
            }

            // ItemsSourceにまとめてバインド
            CityComboBox.ItemsSource = comboItems;
        }

        public MainWindow() {
            InitializeComponent();
            LoadCitiesFromJson(); //都市リスト読み込み
            Loaded += CurrentLocationButton_Click;
        }

        private bool isDarkMode = false;

        private void ThemeToggleButton_Click(object sender, RoutedEventArgs e) {
            if (isDarkMode) {
                // ライトモードに戻す
                this.Background = System.Windows.Media.Brushes.LightBlue; // フォーム背景を白に
                FormName.Foreground = System.Windows.Media.Brushes.Black;
                LastUpdatedText.Foreground = System.Windows.Media.Brushes.Black;

                // アイコンを月に戻す
                ThemeIcon.Source = new BitmapImage(new Uri("https://img.icons8.com/ios-filled/50/crescent-moon.png"));

                isDarkMode = false;
            } else {
                // ダークモードに切り替え
                this.Background = System.Windows.Media.Brushes.Black; // フォーム背景を黒に
                FormName.Foreground = System.Windows.Media.Brushes.White;
                LastUpdatedText.Foreground = System.Windows.Media.Brushes.White;

                // アイコンを太陽に切り替え
                ThemeIcon.Source = new BitmapImage(new Uri("https://img.icons8.com/ios-filled/50/sun--v1.png"));

                isDarkMode = true;
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e) {
            if (CityComboBox.SelectedItem is string city &&
                CityCoordinates.TryGetValue(city, out var coords)) {
                await GetWeatherAsync(coords.Latitude, coords.Longitude, city);
                UpdateLastUpdatedTime();
            }
        }

        private async void CurrentLocationButton_Click(object sender, RoutedEventArgs e) {
            using var http = new HttpClient();
            try {
                var location = await http.GetFromJsonAsync<IpLocation>("http://ip-api.com/json/");
                if (location?.status == "success") {
                    CityComboBox.SelectedItem = null;
                    CityComboBox.Text = string.Empty;

                    await GetWeatherAsync(location.lat, location.lon, location.city);
                    UpdateLastUpdatedTime();
                } else {
                    MessageBox.Show("現在地の取得に失敗しました。");
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"位置情報取得エラー：{ex.Message}");
            }
        }

        public class IpLocation {
            public string status { get; set; }
            public string city { get; set; }
            public double lat { get; set; }
            public double lon { get; set; }
        }

        private async Task GetWeatherAsync(double latitude, double longitude, string cityName) {

            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}" +
             "&current=temperature_2m,wind_speed_10m,relative_humidity_2m,weather_code" +
             "&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,wind_speed_10m,weather_code" +
             "&daily=temperature_2m_max,temperature_2m_min,weather_code" +
             "&timezone=auto";

            using var http = new HttpClient();

            try {
                var weather = await http.GetFromJsonAsync<WeatherResponse>(url);

                if (weather?.current != null) {
                    // 現在の天気を表示
                    CityNameText.Text = $"都市名：{cityName}";
                    CurrentWeatherText.Text = $"天気：{GetWeatherDescription(weather.current.weather_code)}";
                    CurrentTempText.Text = $"気温：{weather.current.temperature_2m} ℃";
                    CurrentHumidityText.Text = $"湿度：{weather.current.relative_humidity_2m} ％";
                    CurrentWindText.Text = $"風速：{weather.current.wind_speed_10m} m/s";
                    WeatherIcon.Source = new BitmapImage(new Uri(GetWeatherIconUrl(weather.current.weather_code)));

                    // 現在時刻に最も近い時間から12時間分の予報を表示
                    var now = DateTime.Now;
                    int startIndex = weather.hourly.time.FindIndex(t => DateTime.Parse(t) >= now);
                    if (startIndex == -1) startIndex = 0;

                    int hoursToShow = 12;
                    int maxCount = Math.Min(hoursToShow, weather.hourly.time.Count - startIndex);

                    var hourlyItems = new List<HourlyForecastItem>();
                    for (int i = 0; i < maxCount; i++) {
                        int index = startIndex + i;
                        hourlyItems.Add(new HourlyForecastItem {
                            Time = DateTime.Parse(weather.hourly.time[index]).ToString("HH:mm"),
                            Temperature = $"{weather.hourly.temperature_2m[index]}℃",
                            Humidity = $"{weather.hourly.relative_humidity_2m[index]}%",
                            Wind = $"{weather.hourly.wind_speed_10m[index]}m/s",
                            Icon = GetWeatherIconUrl(weather.hourly.weather_code[index])
                        });
                    }
                    HourlyForecastPanel.ItemsSource = hourlyItems;

                    if (weather?.daily != null) {
                        var weeklyItems = new List<WeeklyForecastItem>();
                        for (int i = 0; i < weather.daily.time.Count; i++) {
                            weeklyItems.Add(new WeeklyForecastItem {
                                Date = DateTime.Parse(weather.daily.time[i]).ToString("MM/dd"),
                                TempMax = $"最高: {weather.daily.temperature_2m_max[i]}℃",
                                TempMin = $"最低: {weather.daily.temperature_2m_min[i]}℃",
                                Icon = GetWeatherIconUrl(weather.daily.weather_code[i])
                            });
                        }
                        WeeklyForecastPanel.ItemsSource = weeklyItems;
                    }
                } else {
                    MessageBox.Show("天気データが取得できませんでした。");
                }
            }
            catch (Exception ex) {
                MessageBox.Show($"エラー：{ex.Message}");
            }
        }

        private string GetWeatherDescription(int code) {
            return code switch {
                0 => "快晴",
                1 => "晴れ",
                2 => "曇り",
                3 => "霧",
                45 => "霧",
                48 => "霧氷",
                51 => "弱い霧雨",
                61 => "弱い雨",
                71 => "弱い雪",
                80 => "にわか雨",
                95 => "雷雨",
                _ => "不明"
            };
        }

        private string GetWeatherIconUrl(int code) {
            return code switch {
                0 => "https://img.icons8.com/color/48/sun.png",
                1 => "https://img.icons8.com/color/48/partly-cloudy-day.png",
                2 => "https://img.icons8.com/color/48/cloud.png",
                3 => "https://img.icons8.com/color/48/clouds.png",
                45 or 48 => "https://img.icons8.com/color/48/fog-day.png",
                51 or 53 or 55 => "https://img.icons8.com/color/48/light-rain.png",
                61 or 63 or 65 => "https://img.icons8.com/color/48/rain.png",
                66 or 67 => "https://img.icons8.com/color/48/sleet.png",
                71 or 73 or 75 or 77 => "https://img.icons8.com/color/48/snow.png",
                80 or 81 or 82 => "https://img.icons8.com/color/48/rain.png",
                85 or 86 => "https://img.icons8.com/color/48/snow.png",
                95 or 96 or 99 => "https://img.icons8.com/color/48/storm.png",
                _ => "https://img.icons8.com/color/48/weather.png"
            };
        }
    }

    public class DailyForecast {
        public List<string> time { get; set; }
        public List<double> temperature_2m_max { get; set; }
        public List<double> temperature_2m_min { get; set; }
        public List<int> weather_code { get; set; }
    }

    public class WeeklyForecastItem {
        public string Date { get; set; }
        public string TempMax { get; set; }
        public string TempMin { get; set; }
        public string Icon { get; set; }
    }

    public class WeatherResponse {
        public CurrentWeather current { get; set; }
        public HourlyForecast hourly { get; set; }
        public DailyForecast daily { get; set; }
    }

    public class CurrentWeather {
        public string time { get; set; }
        public double temperature_2m { get; set; }
        public double wind_speed_10m { get; set; }
        public double relative_humidity_2m { get; set; }
        public int weather_code { get; set; }
    }

    public class HourlyForecast {
        public List<string> time { get; set; }
        public List<double> temperature_2m { get; set; }
        public List<double> relative_humidity_2m { get; set; }
        public List<double> precipitation_probability { get; set; }
        public List<double> wind_speed_10m { get; set; }
        public List<int> weather_code { get; set; }
    }

    public class HourlyForecastItem {
        public string Time { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string Wind { get; set; }
        public string Icon { get; set; }
    }
}
       