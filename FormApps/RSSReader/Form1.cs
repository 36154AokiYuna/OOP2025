using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace RSSReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        //�������g��������
        //Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
        //    {"��v", "https://news.yahoo.co.jp/rss/topics/top-picks.xml"},
        //    {"�o��", "https://news.yahoo.co.jp/rss/topics/business.xml"},
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
                    //string xml = await hc.GetStringAsync(getRssUrl(comboBox1.Text));
                    string xml = await hc.GetStringAsync(comboBox1.Text);
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
                } else {
                    string xml = await hc.GetStringAsync(urls[comboBox1.SelectedIndex]);
                    XDocument xdoc = XDocument.Parse(xml);

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
                }
            }

            //���X�g�{�b�N�X�̔w�i�F�����݂ɕύX
            //if (items % 2 == 0) {
            //    lbTitles.BackColor = Color.LightGray;
            //}

        }

        //�R���{�{�b�N�X�̕�������`�F�b�N���ăA�N�Z�X�\��URL��ԋp����
        //private string getRssUrl(string str) {
        //    if (rssUrlDict.ContainsKey(str)) {
        //        return rssUrlDict[str];
        //    }
        //    return str;
        //}


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
        //private void wvRssLink_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
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

        //���C�ɓ���o�^
        private void btRssAdd_Click(object sender, EventArgs e) {
            if (comboBox1.Items.Contains(tbFavAdd.Text)) {
                tsslbMessage.Text = "�d�����Ă��܂�";
            } else {
                urls.Add(comboBox1.Text);
                comboBox1.Items.Add(tbFavAdd.Text);
                tsslbMessage.Text = "���C�ɓ���o�^����";
            }
        }

        //���C�ɓ���폜
        private void btRssDel_Click(object sender, EventArgs e) {
            if (comboBox1.Items.Contains(tbFavAdd.Text)) {
                urls.RemoveAt(comboBox1.SelectedIndex);
                comboBox1.Items.Remove(comboBox1.Text);
                tsslbMessage.Text = "���C�ɓ���폜����";
            } else {
                tsslbMessage.Text = "���C�ɓ���ɑ��݂��܂���";
            }
        }
    }
}
