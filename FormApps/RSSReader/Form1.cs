using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace RSSReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        public Form1() {
            InitializeComponent();
        }

        private async void btRssGet_Click(object sender, EventArgs e) {
            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(tbUrl.Text);
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

            //リストボックスの背景色を交互に変更
            //if (items % 2 == 0) {
            //    lbTitles.BackColor = Color.LightGray;
            //}

        }

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
    }
}
