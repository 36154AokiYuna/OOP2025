using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Exercise01_Wpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        //模範解答
        private async void Button_Click(object sender, RoutedEventArgs e) {
            TextArea.Text = await TextReaderSample.ReadTextAsync("吾輩は猫である.txt");
        }

        static class TextReaderSample {
            public static async Task<string> ReadTextAsync(string filePath) {
                var sb = new StringBuilder();
                var sr = new StreamReader(filePath);
                while (!sr.EndOfStream) {
                    var line = await sr.ReadLineAsync();
                    sb.AppendLine(line);
                    await Task.Delay(10);  //わざと遅延させる
                }
                return sb.ToString();
            }
        }
    }
}