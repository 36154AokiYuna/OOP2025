
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("***　変換アプリ　***");
            Console.WriteLine("１：インチからメートル");
            Console.WriteLine("２：メートルからインチ");
            Console.Write("＞" );
            int num = int.Parse(Console.ReadLine());

            Console.Write("はじめ：");　//コンソール入力
            int start = int.Parse(Console.ReadLine());　//文字列で取り込んで整数へ変換
            Console.Write("おわり：");　//コンソール入力
            int end = int.Parse(Console.ReadLine());　//文字列で取り込んで整数へ変換

            if(num == 1){
                PrintInchToMeterList(start, end);
            } else {
                PrintMeterToInchList(start, end);
            }

                //インチからメートルへの対応表を出力
                static void PrintInchToMeterList(int start, int end) {
                    for (int inch = start; inch <= end; inch++) {
                        double meter = InchConverter.ToMeter(inch);
                        Console.WriteLine($"{inch}inch = {meter:0.0000}m");
                    }
                }

                //メートルからインチへの対応表を出力
                static void PrintMeterToInchList(int start, int end) {
                    for (int meter = start; meter <= end; meter++) {
                        double inch = InchConverter.FromMeter(meter);
                        Console.WriteLine($"{meter}m = {inch:0.0000}inch");
                    }
                }
        }
    }
}
