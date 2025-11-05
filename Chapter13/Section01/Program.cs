namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            //var selected = Library.Books
            //    .GroupBy(b => b.PublishedYear)
            //    .Select(group => group.MaxBy(b => b.Price))
            //    .OrderBy(b => b!.PublishedYear);

            //foreach (var book in selected) {
            //    Console.WriteLine($"{book!.PublishedYear}年 {book!.Title} ({book!.Price})");
            //}

            var groups = Library.Categories
                            .GroupJoin(Library.Books
                                    , c => c.Id
                                    , b => b.CategoryId
                                    , (c, books) => new {
                                        Category = c.Name,
                                        Books = books
                                    });

            foreach (var group in groups) {
                Console.WriteLine(group.Category);
                foreach (var book in group.Books) {
                    Console.WriteLine($"   {book.Title} ({book.PublishedYear})年");
                }
            }
        }
    }
}
