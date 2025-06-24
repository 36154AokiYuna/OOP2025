using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var today = new DateTime(2025,7,12); //日付
            var now = DateTime.Now;     //日付と時刻

            Console.WriteLine($"Today:{today.Month}");
            Console.WriteLine($"Now:{now}");

            //①自分の生年月日は何曜日かをプログラムを書いて調べる
            var Day = new DateTime(2006,3,21);
            DayOfWeek dayOfWeek = Day.DayOfWeek;
            Console.WriteLine(dayOfWeek);

            Console.WriteLine("日付を入力");
            Console.Write("西暦：");
            var year = int.Parse(Console.ReadLine());
            Console.Write("月：");
            var month = int.Parse(Console.ReadLine());
            Console.Write("日：");
            var day = int.Parse(Console.ReadLine());

            var date = new DateTime(year, month, day);
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var str = date.ToString("ggyy年M月d日",culture);

            var dayofWeek = culture.DateTimeFormat.GetDayName(date.DayOfWeek);

            Console.WriteLine($"{str}は{dayofWeek}です");

            //③生まれてから〇〇〇〇日
            var dt1 = new DateTime(year, month, day,12,30,52);
            var dt2 = DateTime.Today;
            TimeSpan diff = dt2.Date - dt1.Date; 
            Console.WriteLine($"生まれてから{diff.Days}日"); //diff.TotalDaysでもいける

            //TimeSpan diff;
            //while (true) {
            //    diff = DateTime.Now - dt2;
            //    Console.Write($"\r{diff.TotalSeconds}秒");//生まれてからの経過秒数
            //}

            //④あなたは〇〇歳です！
            var age = dt2.Year - dt1.Year;
            if(dt2 < dt1.AddYears(age)) {
                age--;
            }
            Console.WriteLine($"あなたは{age}歳です！");

            //⑤1月1日から何日目か？
            int dayOfYear = dt1.DayOfYear;
            Console.WriteLine($"1月1日から{dayOfYear}日目");

            //②うるう年の判定プログラムを作成する
            //西暦を入力
            //　→〇〇〇〇年はうるう年です
            //　→〇〇〇〇年は平年です
            Console.Write("西暦を入力：");
            var Year = Console.ReadLine();
            var isLeapYear = DateTime.IsLeapYear(int.Parse(Year));
            if (isLeapYear) {
                Console.WriteLine($"{Year}年はうるう年です");
            } else {
                Console.WriteLine($"{Year}年は平年です");
            }
        }
    }
}
