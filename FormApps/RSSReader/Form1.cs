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
                //XDocument xdoc = XDocument.Load(url); //RSS�̎擾

                //RSS����͂��ĕK�v�ȗv�f���擾
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string)x.Element("title"),
                            Link = (string)x.Element("link"),
                        }).ToList();

                //���X�g�{�b�N�X�փ^�C�g����\��
                lbTitles.Items.Clear();
                items.ForEach(x => lbTitles.Items.Add(x.Title ?? "�f�[�^�Ȃ�"));

                //foreach (var item in items) {
                //    lbTitles.Items.Add(item.Title);
                //}
            }

            //���X�g�{�b�N�X�̔w�i�F�����݂ɕύX
            //if (items % 2 == 0) {
            //    lbTitles.BackColor = Color.LightGray;
            //}

        }

        //�^�C�g����I���i�N���b�N�j�����Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link ?? "https://www.yahoo.co.jp/");
        }



        //�߂�{�^��
        private void button1_Click(object sender, EventArgs e) {
            wvRssLink.GoBack();
        }

        //�i�ރ{�^��
        private void btGoForward_Click(object sender, EventArgs e) {
            wvRssLink.GoForward();
        }

        //�N�����̏���
        private void Form1_Load(object sender, EventArgs e) {
            //�����̉�
            //btGoBack.Enabled = false;
            //btGoForward.Enabled = false;

            //���������@�͔͉�
            GoForwardBtEnableSet();
        }

        //�ǂݍ��ݏI��
        private void wvRssLink_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e) {
            //�����̉�
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

            //���������@�͔͉�
            GoForwardBtEnableSet();
        }

        private void GoForwardBtEnableSet() {
            btGoBack.Enabled = wvRssLink.CanGoBack;
            btGoForward.Enabled = wvRssLink.CanGoForward;
        }
    }
}
