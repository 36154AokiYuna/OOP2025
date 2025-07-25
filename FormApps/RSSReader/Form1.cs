using System.Dynamic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RSSReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        //辞書を使ったやり方
        //Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
        //    {"主要", "https://news.yahoo.co.jp/rss/topics/top-picks.xml"},
        //    {"経済", "https://news.yahoo.co.jp/rss/topics/business.xml"},
        //    {"IT", "https://news.yahoo.co.jp/rss/topics/it.xml" }
        //};

        //private void Form1_Load(object sender, EventArgs e) {
        //    comboBox1.DataSource = rssUrlDict.Select(k => k.Key).ToList();

        //    GoForwardBtEnableSet();
        //}


        List<string> urls = new List<string> {"https://news.yahoo.co.jp/rss/topics/top-picks.xml",
                            "https://news.yahoo.co.jp/rss/topics/business.xml",
                            "https://news.yahoo.co.jp/rss/topics/it.xml"};

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            using (var hc = new HttpClient()) {

                if (comboBox1.Text.Contains("https")) {
                    //URLの形式チェック
                    Uri.IsWellFormedUriString(comboBox1.Text, UriKind.Absolute);

                    try {
                        //string xml = await hc.GetStringAsync(getRssUrl(comboBox1.Text));
                        string xml = await hc.GetStringAsync(comboBox1.Text);
                        XDocument xdoc = XDocument.Parse(xml);

                        //var url = hc.OpenRead(tbUrl.Text);
                        //XDocument xdoc = XDocument.Load(url); //RSSの取得

                        //RSSを解析して必要な要素を取得
                        items = xdoc.Root.Descendants("item")
                            .Select(x =>
                                new ItemData {
                                    Title = (string)x.Element("title"),
                                    Link = (string)x.Element("link"),
                                }).ToList();

                        //リストボックスへタイトルを表示
                        lbTitles.Items.Clear();
                        items.ForEach(x => lbTitles.Items.Add(x.Title ?? "データなし"));

                        //foreach (var item in items) {
                        //    lbTitles.Items.Add(item.Title);
                        //}
                    }
                    catch (Exception) {
                        tsslbMessage.Text = "URLが正しくありません";
                    }
                } else {
                    try {
                        string xml = await hc.GetStringAsync(urls[comboBox1.SelectedIndex]);
                        XDocument xdoc = XDocument.Parse(xml);

                        //RSSを解析して必要な要素を取得
                        items = xdoc.Root.Descendants("item")
                            .Select(x =>
                                new ItemData {
                                    Title = (string)x.Element("title"),
                                    Link = (string)x.Element("link"),
                                }).ToList();

                        //リストボックスへタイトルを表示
                        lbTitles.Items.Clear();
                        items.ForEach(x => lbTitles.Items.Add(x.Title ?? "データなし"));
                    }
                    catch (Exception) {
                        
                    }
                }
            }
        }


        //コンボボックスの文字列をチェックしてアクセス可能なURLを返却する
        //private string getRssUrl(string str) {
        //    if (rssUrlDict.ContainsKey(str)) {
        //        return rssUrlDict[str];
        //    }
        //    return str;
        //}


        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }


        //戻るボタン
        private void button1_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }

        //進むボタン
        private void btGoForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }

        //起動時の処理
        private void Form1_Load(object sender, EventArgs e) {
            //自分の回答
            //btGoBack.Enabled = false;
            //btGoForward.Enabled = false;

            //書き換え　模範解答
            GoForwardBtEnableSet();
        }

        //読み込み終了
        //private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            //自分の回答
            //if (wvRssLink.CanGoBack == true) {
            //    btGoBack.Enabled = true;
            //} else {
            //    btGoBack.Enabled = false;
            //}

            //if (wvRssLink.CanGoForward == true) {
            //    btGoForward.Enabled = true;
            //} else {
            //    btGoForward.Enabled = false;
            //}

            //書き換え　模範解答
            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }

        //お気に入り登録
        private void btRssAdd_Click(object sender, EventArgs e) {
            if (comboBox1.Items.Contains(tbFavAdd.Text)) {
                tsslbMessage.Text = "重複しています";
            } else {
                urls.Add(comboBox1.Text);
                comboBox1.Items.Add(tbFavAdd.Text);
                tsslbMessage.Text = "お気に入り登録完了";
            }
        }

        //お気に入り削除
        private void btRssDel_Click(object sender, EventArgs e) {
            if (comboBox1.Items.Contains(tbFavAdd.Text)) {
                urls.RemoveAt(comboBox1.SelectedIndex);
                comboBox1.Items.Remove(comboBox1.Text);
                tsslbMessage.Text = "お気に入り削除完了";
            } else {
                tsslbMessage.Text = "お気に入りに存在しません";
            }
        }

        //背景色を交互に変更（コピペ）
        private void lbTitles_DrawItem(object sender, DrawItemEventArgs e) {
            var idx = e.Index;                                                      //描画対象の行
            if (idx == -1) return;                                                  //範囲外なら何もしない
            var sts = e.State;                                                      //セルの状態
            var fnt = e.Font;                                                       //フォント
            var _bnd = e.Bounds;                                                    //描画範囲(オリジナル)
            var bnd = new RectangleF(_bnd.X, _bnd.Y, _bnd.Width, _bnd.Height);     //描画範囲(描画用)
            var txt = (string)lbTitles.Items[idx];                                  //リストボックス内の文字
            var bsh = new SolidBrush(lbTitles.ForeColor);                           //文字色
            var sel = (DrawItemState.Selected == (sts & DrawItemState.Selected));   //選択行か
            var odd = (idx % 2 == 1);                                               //奇数行か
            var fore = Brushes.WhiteSmoke;                                         //偶数行の背景色
            var bak = Brushes.AliceBlue;                                           //奇数行の背景色

            e.DrawBackground();                                                     //背景描画

            //奇数項目の背景色を変える（選択行は除く）
            if (odd && !sel) {
                e.Graphics.FillRectangle(bak, bnd);
            } else if (!odd && !sel) {
                e.Graphics.FillRectangle(fore, bnd);
            }

            //文字を描画
            e.Graphics.DrawString(txt, fnt, bsh, bnd);
        }


        //フォームが閉じたら呼ばれる
        
    }
}
