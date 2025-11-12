using System.IO;
using System.Text;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            //10.1
            //var filePath = "./Greeting.txt";
            //if(File.Exists(filePath)) {
            //    using var reader = new StreamReader(filePath, Encoding.UTF8);
            //    while (!reader.EndOfStream) {
            //        var line = reader.ReadLine();
            //        Console.WriteLine(line);
            //    }
            //}

            //10.2
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadAllLines(filePath, Encoding.UTF8);
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}

            //10.3
            //var filePath = "./Greeting.txt";
            //var lines = File.ReadLines(filePath, Encoding.UTF8);
            //foreach (var line in lines) {
            //    Console.WriteLine(line);
            //}

            //10.4
            //var lines = File.ReadLines(filePath)
            //    .Take(10)
            //    .ToArray();

            //10.5
            //var count = File.ReadLines(filePath)
            //    .Count(s => s.Contains("C#"));

            //10.6
            //var lines = File.ReadLines(filePath)
            //    .Where(s => !String.IsNullOrWhiteSpace(s))
            //    .ToArray();

            //10.7
            //var exists = File.ReadLines(filePath)
            //    .Where(s => !String.IsNullOrEmpty(s))
            //    .Any(s => s.All(c => Char.IsAsciiDigit(c)));

            //10.8
            //var lines = File.ReadLines(filePath)
            //    .Distinct()
            //    .OrderBy(s => s.Length)
            //    .ToArray();

            //10.9
            var filePath = "./Greeting.txt";

            var lines = File.ReadLines(filePath)
                .Select((s, ix) => $"{ix + 1,4}: {s}");

            foreach (var line in lines) {
                Console.WriteLine(line);
            }
        }
    }
}
