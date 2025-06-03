
using System.Text;

namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";

            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);
        }

        private static void Exercise1(string text) {
            Console.WriteLine("空白数：" + text.Count(c => c == ' '));
            Console.WriteLine();

            //別解
            //var spaces = text.Count(char.IsWhiteSpace);
        }

        private static void Exercise2(string text) {
            Console.WriteLine(text.Replace("big", "small"));
            Console.WriteLine();
        }

        private static void Exercise3(string text) {
            var words = text.Split(' ');
            var sb = new StringBuilder();

            //for (int i = 0; i < words.Length - 1; i++) {
            //    sb.Append(words[i] + ' ');
            //}
            //Console.WriteLine(sb.Append(words[words.Length-1] + '.'));

            foreach (var word in words) {
                sb.Append(word + ' ');
            }
            var str = sb.ToString();
            Console.WriteLine(str.TrimEnd() + '.');
            Console.WriteLine();
        }

        private static void Exercise4(string text) {
            var words = text.Split(' ');
            Console.WriteLine("単語数：" + words.Count());
            Console.WriteLine();

            //別解
            //var count = text.Split(' ').Length:
            //Console.WriteLine("単語数：{0}",count);
        }

        private static void Exercise5(string text) {
            var words = text.Split(' ');
            foreach (var word in words.Where(s => s.Length <= 4)) {
                Console.WriteLine(word);
            }
        }
    }
}
