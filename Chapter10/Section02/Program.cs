namespace Section02 {
    internal class Program {
        static void Main(string[] args) {

            //10.10
            //var filePath = "./Example/いろは歌.txt";
            //using (var writer = new StreamWriter(filePath)) {
            //    writer.WriteLine("色はにほへど　散りぬるを");
            //    writer.WriteLine("我が世たれぞ　常ならむ");
            //    writer.WriteLine("有為の奥山　今日超えて");
            //    writer.WriteLine("浅き夢見じ　酔ひもせず");
            //}

            //10.11
            //var lines = new[] { "====", "京の夢", "大坂の夢", };
            //var filePath = "./Example/いろは歌.txt";
            //using (var writer = new StreamWriter(filePath, append: true)) {
            //    foreach (var line in lines) {
            //        writer.WriteLine(line);
            //    }
            //}

            //10.12
            //var lines = new[] { "Tokyo", "New Delhi", "Bangkok", "London", "Paris",  };
            //var filePath = "./Example/Cities.txt";
            //File.WriteAllLines(filePath, lines);

            //10.13
            //var names = new List<string> {
            //    "Tokyo", "New Delhi", "Bangkok", "London", "Paris", "Berlin", "Canberra", "HongKong"
            //};
            //var filePath = "./Example/Cities.txt";
            //File.WriteAllLines(filePath, names.Where(s => s.Length > 5));

            //10.14
            var filePath = "./Example/いろは歌.txt";

            using var stream = new FileStream(filePath, FileMode.Open,
                FileAccess.ReadWrite, FileShare.None);
            //FileMode.Open→既存ファイルをオープン
            //FileAccess.ReadWrite→読み込みと書き込みの両方を行えるようにする
            //FIleShare.None→他からのアクセスを拒否する

            using var reader = new StreamReader(stream);
            using var writer = new StreamWriter(stream);

            string texts = reader.ReadToEnd();
            stream.Position = 0;
            writer.WriteLine("挿入する新しい行１");
            writer.WriteLine("挿入する新しい行２");
            writer.WriteLine("texts");
        }
    }
}
