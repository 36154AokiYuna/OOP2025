﻿namespace PrefCapitalLocationSystem {
    internal class Program {
        static private Dictionary<string, string> prefOfficeDict = new Dictionary<string, string>();

        //列挙型の定義
        public enum SelectMenu {
            Alldisp = 1,
            Search = 2,
            Exit = 9
        }

        static void Main(string[] args) {
            String? pref, prefCaptalLocation;

            //入力処理
            Console.WriteLine("県庁所在地の登録【入力終了：Ctrl + 'Z'】");

            while (true) {
                //①都道府県の入力
                Console.Write("都道府県:");
                pref = Console.ReadLine();

                if (pref == null) break;    //無限ループを抜ける(Ctrl + 'Z')

                //県庁所在地の入力
                Console.Write("県庁所在地:");
                prefCaptalLocation = Console.ReadLine();

                //既に都道府県が登録されているか？
                //ヒント：ContainsKeyを使用して調べる

                //登録済みなら確認して上書き処理、上書きしない場合はもう一度都道府県の入力…①へ
                //ヒント：Console.WriteLine("上書きしますか？(Y/N)");

                //*　ここに入力　*******************//
                if (prefOfficeDict.ContainsKey(pref)) {
                    Console.Write("上書きしますか？(Y/N)");
                    if(Console.ReadLine() == "N") {
                        continue;
                    }
                }

                //県庁所在地登録処理
                //*　ここに入力　*******************//
                //模範解答ではnullの場合も考える
                prefOfficeDict[pref] = prefCaptalLocation ?? "未入力";


                Console.WriteLine();//改行
            }

            Boolean endFlag = false;    //終了フラグ（無限ループを抜け出す用）
            while (!endFlag) {
                switch (menuDisp()) {
                    case SelectMenu.Alldisp:  //一覧出力処理
                        allDisp();
                        break;
                    case SelectMenu.Search:  //検索処理
                        searchPrefCaptalLocation();
                        break;
                    case SelectMenu.Exit:  //無限ループを抜ける
                        endFlag = true;
                        break;
                }
            }
        }

        //メニュー表示
        private static SelectMenu menuDisp() {
            Console.WriteLine("\n**** メニュー ****");
            Console.WriteLine("1：一覧表示");
            Console.WriteLine("2：検索");
            Console.WriteLine("9：終了");
            Console.Write(">");

            var menuSelect = (SelectMenu) int.Parse(Console.ReadLine());
            return menuSelect;
        }


        //一覧表示処理
        private static void allDisp() {

            //*　ここに入力　*******************//
            foreach (var item in prefOfficeDict) {
                Console.WriteLine("{0}の県庁所在地は{1}です。",item.Key,item.Value);
            }
        }

        //検索処理
        private static void searchPrefCaptalLocation() {
            Console.Write("都道府県:");
            String? searchPref = Console.ReadLine();
            //*　ここに入力　*******************//
            if (searchPref is null) return;
            Console.WriteLine(searchPref + "の県庁所在地は" + prefOfficeDict[searchPref] + "です");
        }
    }
}
