namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            if (int.TryParse(Console.ReadLine(), out var num)) {
                if (num < 0) {
                    Console.WriteLine(num);
                } else if (num < 100) {
                    Console.WriteLine(num * 2);
                } else if (num < 500) {
                    Console.WriteLine(num * 3);
                } else {
                    Console.WriteLine(num);
                }
            } else {
                Console.WriteLine("入力値に誤りがあります");
            }      
        }
    }
}
