using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        MyColor currentColor; //現在設定している色情報

        public MainWindow() {

            InitializeComponent();
            DataContext = GetColorList();
        }

        /// <summary>
        /// すべての色を取得するメソッド
        /// </summary>
        /// <returns></returns>
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(i => new MyColor() { Color = (Color)i.GetValue(null), Name = i.Name }).ToArray();
        }


        //すべてのスライダーから呼ばれるイベントハンドラ
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            //colorAreaの色（背景色）は、スライダーで指定したRGBの色を表示する

            //自分の回答
            //colorArea.Background = new SolidColorBrush(Color.FromRgb
            //                    ((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value));


            //スライダーの色情報をもとに色の名前を取得する
            //模範解答
            currentColor = new MyColor {
                Color = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value),
                Name = ((MyColor[])DataContext).Where(c => c.Color.R == (byte)rSlider.Value &&
                                                           c.Color.G == (byte)gSlider.Value &&
                                                           c.Color.B == (byte)bSlider.Value)
                                               .Select(x => x.Name).FirstOrDefault(),
            };
            colorArea.Background = new SolidColorBrush(currentColor.Color);
        }

        private void stockButton_Click(object sender, RoutedEventArgs e) {
            //自分の回答
            //var comboSelectMyColor = (MyColor)((ComboBox)colorSelectComboBox).SelectedItem;
            //if (comboSelectMyColor is null) {
            //    stockList.Items.Add($"Ｒ：{rValue.Text}　Ｇ：{gValue.Text}　Ｂ：{bValue.Text}");
            //} else {
            //    stockList.Items.Add(comboSelectMyColor.Name);
            //}


            //模範解答
            //stockList.Items.Insert(0,currentColor);


            //自分の回答
            if (stockList.Items.Contains(currentColor)) {
                MessageBox.Show("既に登録済みです。");
            } else {
                stockList.Items.Insert(0, currentColor);
            }
        }

        //コンボボックスから色を選択
        private void colorSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var comboSelectMyColor = (MyColor)((ComboBox)sender).SelectedItem;

            setSliderValue(comboSelectMyColor.Color); //スライダーを設定

            //模範解答
            currentColor = comboSelectMyColor;
        }

        //各スライダーの値を設定する
        private void setSliderValue(Color color) {
            rSlider.Value = color.R;
            gSlider.Value = color.G;
            bSlider.Value = color.B;
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e) {
            stockList.Items.Remove(stockList.SelectedItem);
        }
    }
}
