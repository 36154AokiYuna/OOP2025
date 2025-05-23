
using System.Xml.Linq;

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
            var lang = langs.Where(s => s.Contains('S'));
            foreach (var name in lang) {
                Console.WriteLine(name);
            }
            Console.WriteLine("");  //改行

            //for文
            for (int i = 0; i < langs.Count; i++) {
                if (langs[i].Contains('S')) {
                    Console.WriteLine(langs[i]);
                }
            }
            Console.WriteLine("");  //改行

            //While文
            int k = 0;
            while (k < langs.Count) {
                if (langs[k].Contains('S')) {
                    Console.WriteLine(langs[k]);
                }
                k++;
            }
        }

        private static void Exercise2(List<string> langs) {
            //foreach文
            foreach (var lan in langs) {
                if (lan.Contains('S')) {
                    Console.WriteLine(lan);
                }
            }
            Console.WriteLine("");  //改行

            //for文
            var lang = langs.Where(s => s.Contains('S')).ToList();
            for(int i = 0; i < lang.Count; i++) {
                Console.WriteLine(lang[i]);
            }
            Console.WriteLine("");  //改行

            //While文
            var lange = langs.Where(s => s.Contains('S')).ToList();
            int k = 0;
            while (k < lange.Count) {
                    Console.WriteLine(lange[k]);
                k++;
            }
        }

        private static void Exercise3(List<string> langs) {
            //2行で完結させる
            var lang = langs.Find(s => s.Length == 10) ?? "unknown";
            Console.WriteLine(lang);
        }
    }
}
