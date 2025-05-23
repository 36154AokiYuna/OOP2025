
namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            List<string> langs = [
                "C#", "Java", "Ruby", "PHP", "Python", "TypeScript",
                "JavaScript", "Swift", "Go",
            ];

            Exercise1(langs);
            Console.WriteLine("---");
            Exercise2(langs);
            Console.WriteLine("---");
            Exercise3(langs);
        }

        private static void Exercise1(List<string> langs) {
            //foreach文
            var lang = langs.Where(l => l.Contains("S"));
            foreach (var name in lang) {
                Console.WriteLine(name);
            }

            //for文
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains("S")) {
                    Console.WriteLine(langs[i]);
                }
            }

            //While文
            int k = 0;
            while (k < langs.Count) {
                if (langs[k].Contains("S")) {
                    Console.WriteLine(langs[k]);
                }
                k++;
            }
        }

        private static void Exercise2(List<string> langs) {

        }

        private static void Exercise3(List<string> langs) {

        }
    }
}
