namespace ProductSample {
    internal class Program {
        static void Main(string[] args) {

            Product karinto = new Product(123, "かりんとう", 180);
            Product daifuku = new Product(124, "大福", 200);


            //税抜きの価格を表示【かりんとうの税抜き価格は〇〇円です】
            Console.WriteLine(karinto.Name + "の税抜き価格は" + karinto.Price + "円です");

            //消費税額の表示【かりんとうの消費税額は〇〇円です】
            Console.WriteLine(karinto.Name + "の消費税額は" + karinto.GetTax() + "円です");

            //税込み価格の表示【かりんとうの税込み価格は〇〇円です】
            Console.WriteLine(karinto.Name + "の税込み価格は" + karinto.GetPriceIncludingTax() + "円です");


            //税抜きの価格を表示【大福の税抜き価格は〇〇円です】
            Console.WriteLine(daifuku.Name + "の税抜き価格は" + daifuku.Price + "円です");

            //消費税額の表示【大福の消費税額は〇〇円です】
            Console.WriteLine(daifuku.Name + "の消費税額は" + daifuku.GetTax() + "円です");

            //税込み価格の表示【大福の税込み価格は〇〇円です】
            Console.WriteLine(daifuku.Name + "の税込み価格は" + daifuku.GetPriceIncludingTax() + "円です");

        }
    }
}
