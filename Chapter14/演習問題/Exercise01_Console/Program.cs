using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

internal class Program {
    static async Task Main(string[] args) {
        string text = await TextReaderSample.ReadText("吾輩は猫である.txt");
        Console.WriteLine(text);

        Console.WriteLine("End!");
    }

    static class TextReaderSample {
        public static async Task<string> ReadText(string filePath) {
            var sb = new StringBuilder();

            // using を使って自動的にファイルを閉じる
            using (var sr = new StreamReader(filePath, Encoding.UTF8)) {
                while (!sr.EndOfStream) {
                    string? line = await sr.ReadLineAsync();  // 1行読み取り
                    sb.AppendLine(line);
                }
            }

            return sb.ToString();
        }
    }
}
