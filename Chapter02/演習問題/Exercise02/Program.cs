
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {


            //インチからメートルへの対応表を出力
            for (int inch = 1; inch <= 10; inch++) {
                double meter = inch * 0.0254;
                Console.WriteLine($"{inch}inch = {meter:0.0000}m");
            }
        }
    }
}
