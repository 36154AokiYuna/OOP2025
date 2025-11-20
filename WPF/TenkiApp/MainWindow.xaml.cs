using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace TenkiApp {
    public partial class MainWindow : Window {

    private readonly Dictionary<string, (double Latitude, double Longitude)> CityCoordinates =
    new Dictionary<string, (double, double)> {
        { "札幌", (43.0642, 141.3468) },
        { "青森", (40.8246, 140.7400) },
        { "盛岡", (39.7036, 141.1527) },
        { "仙台", (38.2688, 140.8721) },
        { "秋田", (39.7186, 140.1024) },
        { "山形", (38.2404, 140.3633) },
        { "福島", (37.7608, 140.4747) },
        { "水戸", (36.3659, 140.4719) },
        { "宇都宮", (36.5658, 139.8836) },
        { "前橋", (36.3912, 139.0609) },
        { "さいたま", (35.8617, 139.6455) },
        { "千葉", (35.6074, 140.1065) },
        { "東京", (35.6895, 139.6917) },
        { "横浜", (35.4437, 139.6380) },
        { "新潟", (37.9026, 139.0232) },
        { "富山", (36.6953, 137.2113) },
        { "金沢", (36.5947, 136.6256) },
        { "福井", (36.0652, 136.2216) },
        { "甲府", (35.6639, 138.5683) },
        { "長野", (36.6513, 138.1810) },
        { "岐阜", (35.4233, 136.7606) },
        { "静岡", (34.9756, 138.3828) },
        { "名古屋", (35.1815, 136.9066) },
        { "津", (34.7303, 136.5086) },
        { "大津", (35.0045, 135.8686) },
        { "京都", (35.0116, 135.7681) },
        { "大阪", (34.6937, 135.5023) },
        { "神戸", (34.6901, 135.1955) },
        { "奈良", (34.6851, 135.8048) },
        { "和歌山", (34.2260, 135.1675) },
        { "鳥取", (35.5011, 134.2351) },
        { "松江", (35.4723, 133.0505) },
        { "岡山", (34.6553, 133.9198) },
        { "広島", (34.3853, 132.4553) },
        { "山口", (34.1861, 131.4706) },
        { "徳島", (34.0658, 134.5593) },
        { "高松", (34.3428, 134.0466) },
        { "松山", (33.8392, 132.7657) },
        { "高知", (33.5597, 133.5311) },
        { "福岡", (33.5902, 130.4017) },
        { "佐賀", (33.2635, 130.3009) },
        { "長崎", (32.7503, 129.8777) },
        { "熊本", (32.7898, 130.7417) },
        { "大分", (33.2382, 131.6126) },
        { "宮崎", (31.9111, 131.4239) },
        { "鹿児島", (31.5966, 130.5571) },
        { "那覇", (26.2124, 127.6809) }
    };

        public MainWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e) {
            CityComboBox.ItemsSource = CityCoordinates.Keys;
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e) {
            if (CityComboBox.SelectedItem is string city &&
                CityCoordinates.TryGetValue(city, out var coords)) {
                await GetWeatherAsync(coords.Latitude, coords.Longitude, city);
            }
        }

        private async Task GetWeatherAsync(double latitude, double longitude, string cityName) {
            string url = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}" +
                         "&current=temperature_2m,wind_speed_10m,relative_humidity_2m,weather_code" +
                         "&hourly=temperature_2m,relative_humidity_2m,precipitation_probability,wind_speed_10m,weather_code" +
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
                0 => "https://openweathermap.org/img/wn/01d.png", // 快晴
                1 => "https://openweathermap.org/img/wn/02d.png", // 晴れ
                2 => "https://openweathermap.org/img/wn/03d.png", // 曇り
                3 => "https://openweathermap.org/img/wn/50d.png", // 霧
                61 => "https://openweathermap.org/img/wn/09d.png", // 雨
                71 => "https://openweathermap.org/img/wn/13d.png", // 雪
                95 => "https://openweathermap.org/img/wn/11d.png", // 雷雨
                _ => "https://openweathermap.org/img/wn/01d.png"   // デフォルト
            };
        }
    }

    public class WeatherResponse {
        public CurrentWeather current { get; set; }
        public HourlyForecast hourly { get; set; }
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