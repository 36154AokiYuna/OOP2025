using System.Diagnostics.Tracing;

namespace Exercise04 {
    internal class Program {
        static void Main(string[] args) {
            var line = "Novelist=谷崎潤一郎;BestWork=春琴抄;Born=1886";

            var text = line.Split(';');
            foreach (var item in text) {
                var words = item.Split('=');
                Console.WriteLine($"{ToJapanese(words[0])}:{words[1]}");
            }
  
        }

        ///<summary>
        ///引数の単語を日本語へ変換します
        ///</summary>
        ///<param name="key">"Novelist","Bestwork","Born"</param>
        ///<returns>"「作家」,「代表作」,「誕生年」</returns>
        static string ToJapanese(string key) {

            return key switch {
                "Novelist" => "作家",
                "BestWork" => "代表作",
                "Born" => "誕生年",
                _ => "引数keyは、正しい値ではありません"
            };

            return "";//エラーをなくすためのダミー
        }
    }
}
