
using System.Security.Cryptography.X509Certificates;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Exercise1_2();
            Console.WriteLine();
            Exercise1_3();
            Console.WriteLine();
            Exercise1_4();
            Console.WriteLine();
            Exercise1_5();
            Console.WriteLine();
            Exercise1_6();
            Console.WriteLine();
            Exercise1_7();
            Console.WriteLine();
            Exercise1_8();

            Console.ReadLine();
        }

        private static void Exercise1_2() {
            var book = Library.Books.MaxBy(b => b.Price);
            Console.WriteLine(book);
        }

        private static void Exercise1_3() {
            //模範解答
            var results = Library.Books
                            .GroupBy(b => b.PublishedYear)
                            .OrderBy(b => b.Key)
                            .Select(b => new {
                                PublishedYear = b.Key,
                                Count = b.Count()
                            });

            foreach (var item in results) {
                Console.WriteLine($"{item.PublishedYear}:{item.Count}");
            }

            //自分の回答
            //var selected = Library.Books
            //                .GroupBy(b => b.PublishedYear)
            //                .Select(g => g.Count());

            //foreach (var item in selected) {
            //    Console.WriteLine($"{}:{}");
            //}

        }

        private static void Exercise1_4() {

        }

        private static void Exercise1_5() {

        }

        private static void Exercise1_6() {

        }

        private static void Exercise1_7() {

        }

        private static void Exercise1_8() {

        }
    }
}
