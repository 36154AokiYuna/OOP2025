using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            Console.Write("パスを入力してください：");
            string path = Console.ReadLine();
            TextProcessor.Run<LineCounterProcessor>(path);
        }
    }
}
