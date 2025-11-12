namespace Section04 {
    internal class Program {
        static void Main(string[] args) {

            //10.32
            if (Directory.Exists("./Example")) {
                Console.WriteLine("存在しています");
            } else {
                Console.WriteLine("存在していません");
            }

            //10.33
            //DirectoryInfo di = Directory.CreateDirectory("./Example");

            //10.34
            //DirectoryInfo di = Directory.CreateDirectory("./Example/temp");

            //10.35
            //var di = new DirectoryInfo("./Example");
            //di.Create();

            //DirectoryInfoクラスを使いサブディレクトリを作成する
            DirectoryInfo di = Directory.CreateDirectory("./Example");
            DirectoryInfo sdi = di.CreateSubdirectory("temp");

            //10.36
            Directory.Delete("./Example/temp");

            //10.37
            Directory.Delete("./Example/temp", recursive: true);

            //10.38
            di.Delete(recursive: true);

            //10.39
            Directory.Move("./example/temp", "./MyWork");

            //10.40
            di.MoveTo("./MyWork");

            //10.41
            Directory.Move("./Example/temp", "./Example/save");

            //10.42
            di.MoveTo("./Example/save");
        }
    }
}
