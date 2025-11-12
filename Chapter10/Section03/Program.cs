namespace Section03 {
    internal class Program {
        static void Main(string[] args) {
            
            //10.15
            if (File.Exists("./Example/Greeting.txt")) {
                Console.WriteLine("すでに存在しています。");
            }

            //10.17
            File.Delete("./Example/Greeting.txt");

            //10.19
            //既存ファイルを上書きしてよい場合第三引数に→overwrite: true
            File.Copy("./Example/src/Greeting.txt", "./Example/dest/Greeting.txt");

            //10.21
            File.Move("./Example/src/Greeting.txt", "./Example/dest/Greeting.txt");

            //10.23
            File.Move("./Example/oldfile.txt", "./Example/newfile.txt");

            //10.25
            var lastWriteTime = File.GetLastWriteTime("./Example/Greeting.txt");

            //10.26
            File.SetLastWriteTime("./Example/Greeting.txt", DateTime.Now);


            //10.16
            var fi = new FileInfo("./Example/Greeting.txt");
            if(fi.Exists) {
                Console.WriteLine("すでに存在しています。");
            }

            //10.18
            fi.Delete();

            //10.20
            FileInfo dup = fi.CopyTo("./Example/dest/Greeting.txt", overwrite: true);

            //10.22
            fi.MoveTo("./Example/dest/Greeting.txt");

            //10.24
            fi.MoveTo("./Example/newfile.txt");

            //10.27
            DateTime lastWriteTime2 = fi.LastWriteTime;

            //10.28
            fi.LastWriteTime = DateTime.Now;

            //10.29
            var finfo = new FileInfo("./Example/Greeting.txt");
            DateTime lastCreationTime = finfo.CreationTime;

            //10.30
            long size = fi.Length;

            //10.31
            if(fi.Length == 0) {
                fi.Delete();
            }
        }
    }
}
