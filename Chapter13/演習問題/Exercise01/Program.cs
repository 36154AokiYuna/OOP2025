
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
            //P.299参照
            var books = Library.Books
                        .OrderByDescending(b => b.PublishedYear)
                        .ThenByDescending(b => b.Price);

            foreach (var book in books) {
                Console.WriteLine($"{book.PublishedYear}年 {book.Price}円 {book.Title}");
            }
        }

        private static void Exercise1_5() {
            //自分の回答
            var books = Library.Books
                .Join(Library.Categories
                        , book => book.CategoryId
                        , Category => Category.Id
                        , (book, category) => new {
                            book.Title,
                            Category = category.Name,
                            book.PublishedYear
                        })
                .Where(b => b.PublishedYear == 2022)
                .Select(b => b.Category)
                .Distinct();

            foreach (var book in books) {
                Console.WriteLine(book);
            }

            //模範解答
            //var categoryNames = Library.Books
            //                        .Where(b=>b.PublishedYear == 2022)
            //                        .Join(Library.Categories
            //                                , b => b.CategoryId
            //                                , c => c.Id
            //                                , (b, c) => c.Name)
            //                                .Distinct();

            //foreach (var name in categoryNames) {
            //    Console.WriteLine(name);
            //}
        }

        private static void Exercise1_6() {
            //自分の回答
            var books = Library.Books
                .Join(Library.Categories
                        , b => b.CategoryId
                        , c => c.Id
                        , (b, c) => new {
                            b.Title,
                            Category = c.Name,
                            b.PublishedYear
                        })
                .GroupBy(b => b.Category)
                .OrderBy(b => b.Key);

            foreach (var book in books) {
                Console.WriteLine();
                Console.WriteLine($"# {book.Key}");
                foreach (var title in book) {
                    Console.WriteLine($"   {title.Title}");
                }
            }

            //模範解答
            var groups = Library.Books
                .Join(Library.Categories
                        , b => b.CategoryId
                        , c => c.Id
                        , (b, c) => new {
                            CategoryName = c.Name,
                            b.Title
                        }
                )
                .GroupBy(x => x.CategoryName)
                .OrderBy(x => x.Key);

            foreach (var group in groups) {
                Console.WriteLine();
                Console.WriteLine($"# {group.Key}");
                foreach (var book in group) {
                    Console.WriteLine($"   {book.Title}");
                }
            }
        }

        private static void Exercise1_7() {
            //自分の回答
            var books = Library.Books
                .Join(Library.Categories
                        , b => b.CategoryId
                        , c => c.Id
                        , (b, c) => new {
                            b.Title,
                            Category = c.Name,
                            b.PublishedYear
                        })
                .Where(b => b.Category == "Development")
                .GroupBy(b => b.PublishedYear);

            foreach (var book in books) {
                Console.WriteLine();
                Console.WriteLine($"# {book.Key}");
                foreach (var title in book) {
                    Console.WriteLine($"   {title.Title}");
                }
            }

            //模範解答
            //var groups = Library.Categories
            //    .Where(x => x.Name.Equals("Development"))
            //    .Join(Library.Books
            //            , c => c.Id
            //            , b => b.CategoryId
            //            , (c, b) => new {
            //                b.Title,
            //                b.PublishedYear
            //            })
            //    .GroupBy(x => x.PublishedYear)
            //    .OrderBy(x => x.Key);

            //foreach (var group in groups) {
            //    Console.WriteLine();
            //    Console.WriteLine($"# {group.Key}");
            //    foreach (var book in group) {
            //        Console.WriteLine($"   {book.Title}");
            //    }
            //}
        }

        private static void Exercise1_8() {
            //欠席してたためコピー
            var categoryNames = Library.Categories
                .GroupJoin(Library.Books,
                            c => c.Id,
                            b => b.CategoryId,
                            (c, books) => new {
                                CategoryName = c.Name,
                                Count = books.Count(),
                            })
                .Where(x => x.Count >= 4)
                .Select(x => x.CategoryName);

            foreach (var name in categoryNames) {
                Console.WriteLine(name);
            }
        }
    }
}
