
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);

        }

        private static void Exercise1(string text) {
            var textDict = new Dictionary<Char, int>();
            foreach (var ch in text.ToUpper()) {
                if('A' <= ch && ch <= 'Z') {
                    if (textDict.ContainsKey(ch)) {
                        textDict[ch] += 1;
                    } else {
                        textDict[ch] = 1;
                    }
                }
            }
            var sort = textDict.OrderBy(c => c.Key).ToList();
            foreach (var item in sort) {
                Console.WriteLine($"{item.Key}:{item.Value}");
            }
        }

        private static void Exercise2(string text) {
            
        }
    }
}
