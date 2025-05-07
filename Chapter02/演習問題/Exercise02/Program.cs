
namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("***　変換アプリ　***");
            Console.WriteLine("１：ヤードからメートル");
            Console.WriteLine("２：メートルからヤード");
            Console.Write("＞" );
            int num = int.Parse(Console.ReadLine());

            if(num == 1){
                
            } else {
                
            }

            ////ヤードからメートルへの対応表を出力
            //static void PrintYardToMeterList(int yard) {
            //    Console.Write("変更前（ヤード）："); //コンソール入力
            //    int yard = int.Parse(Console.ReadLine());　//文字列で取り込んで整数へ変換
            //    Console.Write("変更後（メートル）：");
            //    double meter = yard * 0.9144;
            //    Console.WriteLine(meter);
            //}

            ////メートルからヤードへの対応表を出力
            //static void PrintMeterToYardList(int meter) {
            //    Console.Write("変更前（メートル）："); //コンソール入力
            //    int meter = int.Parse(Console.ReadLine());　//文字列で取り込んで整数へ変換
            //    Console.Write("変更後（ヤード）：");
            //    double yard = meter / 0.9144;
            //    Console.WriteLine(yard);
            //}
        }
    }
}
