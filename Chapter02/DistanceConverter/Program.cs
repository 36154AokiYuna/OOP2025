using System.Diagnostics.Metrics;
using System.Xml.Serialization;

namespace DistanceConverter {
    internal class Program {
        //コマンドライン引数で指定された範囲のフィートとメートルの対応表を出力する
        static void Main(string[] args) {

            int start = int.Parse(args[1]);
            int end = int.Parse(args[2]);

            if (args.Length >= 1 && args[0] == "-tom") {  //エラーがない前提ならargs[0] == "-tom"でOK
                PrintFeetToMeterList(start, end);
            } else {
                PrintMeterToFeetList(start, end);
            }

            //フィートからメートルへの対応表を出力
            static void PrintFeetToMeterList(int start, int end) {
                FeetConverter fc = new FeetConverter();
                for (int feet = start; feet <= end; feet++) {
                    //double meter = feet * 0.3048;
                    double meter = fc.ToMeter(feet);
                    Console.WriteLine($"{feet}ft = {meter:0.0000}m");
                }
            }

            //メートルからフィートへの対応表を出力
            static void PrintMeterToFeetList(int start, int end) {
                FeetConverter fc = new FeetConverter();
                for (int meter = start; meter <= end; meter++) {
                    //double meter = feet * 0.3048;
                    double feet = fc.FromMeter(meter);
                    Console.WriteLine($"{meter}m = {feet:0.0000}ft");
                }
            }
        }
    }
}
