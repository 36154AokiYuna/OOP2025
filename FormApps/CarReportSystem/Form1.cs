using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //カーレポート管理用リスト
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

        //設定クラスのインスタンスを生成
        Settings settings = Settings.getInstance();

        public Form1() {
            InitializeComponent();
            dgvRecord.DataSource = listCarReports;
        }

        private void btPicOpen_Click(object sender, EventArgs e) {
            if (ofdPicFileOpen.ShowDialog() == DialogResult.OK) {
                pbPicture.Image = Image.FromFile(ofdPicFileOpen.FileName);
            }
        }

        private void btPicDelete_Click(object sender, EventArgs e) {
            pbPicture.Image = null;
        }

        private void btRecordAdd_Click(object sender, EventArgs e) {

            tsslbMessage.Text = string.Empty;
            var carReport = new CarReport {
                Date = dtpDate.Value,
                Author = cbAuthor.Text,
                Maker = getRadioButtonMaker(),
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
            };

            if (cbAuthor.Text == string.Empty || cbCarName.Text == string.Empty) {
                tsslbMessage.Text = "記録者、または車名が未入力です";
                return;
            }

            listCarReports.Add(carReport);
            setObAuthor(cbAuthor.Text); //コンボボックスへ登録
            setCbCarName(cbCarName.Text);
            InputItemsAllClear(); //登録後は項目をクリア
        }

        //入力項目をすべてクリア
        private void InputItemsAllClear() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = "";
            rbOther.Checked = true;
            cbCarName.Text = "";
            tbReport.Text = "";
            pbPicture.Image = null;
        }

        private CarReport.MakerGroup getRadioButtonMaker() {
            if (rbToyota.Checked) {
                return CarReport.MakerGroup.トヨタ;
            } else if (rbNissan.Checked) {
                return CarReport.MakerGroup.日産;
            } else if (rbHonda.Checked) {
                return CarReport.MakerGroup.ホンダ;
            } else if (rbSubaru.Checked) {
                return CarReport.MakerGroup.スバル;
            } else if (rbImport.Checked) {
                return CarReport.MakerGroup.輸入車;
            } else if (rbOther.Checked) {
                return CarReport.MakerGroup.その他;
            } else {
                return CarReport.MakerGroup.なし;
            }
        }

        private void dgvRecord_Click(object sender, EventArgs e) {
            if (dgvRecord.CurrentRow is null) return;

            dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
            cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
            setRadioButtonMaker((CarReport.MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
            cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
            tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
            pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
        }

        //指定したメーカーのラジオボタンをセット
        private void setRadioButtonMaker(CarReport.MakerGroup targetMaker) {
            switch (targetMaker) {
                case CarReport.MakerGroup.トヨタ:
                    rbToyota.Checked = true;
                    break;
                case CarReport.MakerGroup.日産:
                    rbNissan.Checked = true;
                    break;
                case CarReport.MakerGroup.ホンダ:
                    rbHonda.Checked = true;
                    break;
                case CarReport.MakerGroup.スバル:
                    rbSubaru.Checked = true;
                    break;
                case CarReport.MakerGroup.輸入車:
                    rbImport.Checked = true;
                    break;
                default:
                    rbOther.Checked = true;
                    break;
            }
        }

        //記録者の履歴をコンボボックスへ登録（重複なし）
        private void setObAuthor(string author) {
            //既に登録済みか確認
            if (!cbAuthor.Items.Contains(author)) {
                //未登録なら登録【登録済みなら何もしない】
                cbAuthor.Items.Add(author);
            }
        }

        //車名の履歴をコンボボックスへ登録（重複なし）
        private void setCbCarName(string carName) {
            if (!cbCarName.Items.Contains(carName)) {
                cbCarName.Items.Add(carName);
            }
        }

        //新規入力のイベントハンドラ
        private void btNewRecord_Click(object sender, EventArgs e) {
            InputItemsAllClear();
        }

        //修正ボタンのイベントハンドラ
        private void btRecordModify_Click(object sender, EventArgs e) {
            //模範解答
            //if((dgvRecord.CurrentRow is null)
            //    || (!dgvRecord.CurrentRow.Selected)) return;
            //もしくは
            //if (dgvRecord.Rows.Count == 0) return;
            var index = dgvRecord.CurrentRow.Index;
            listCarReports[index].Date = dtpDate.Value;
            listCarReports[index].Author = cbAuthor.Text;
            listCarReports[index].Maker = getRadioButtonMaker();
            listCarReports[index].CarName = cbCarName.Text;
            listCarReports[index].Report = tbReport.Text;
            listCarReports[index].Picture = pbPicture.Image;

            dgvRecord.Refresh(); //データグリッドビューの更新
        }

        //削除ボタンのイベントハンドラ
        private void btRecordDelete_Click(object sender, EventArgs e) {
            //カーレポート管理用リストから該当するデータを削除する
            //選択されているインデックスを取得
            if (dgvRecord.Rows.Count > 0) {
                var index = dgvRecord.CurrentRow.Index;
                listCarReports.RemoveAt(index);
                InputItemsAllClear();
            }

            //模範解答
            //選択されていない場合は処理を行わない
            //if((dgvRecord.CurrentRow is null)
            //    || (!dgvRecord.CurrentRow.Selected)) return;

            //var index = dgvRecord.CurrentRow.Index;
            //listCarReports.RemoveAt(index);
        }

        private void Form1_Load(object sender, EventArgs e) {
            InputItemsAllClear();

            //交互に色を設定（データグリッドビュー）
            dgvRecord.DefaultCellStyle.BackColor = Color.LightGray;
            dgvRecord.AlternatingRowsDefaultCellStyle.BackColor = Color.WhiteSmoke;

            //設定ファイルを読み込み背景色を設定する（逆シリアル化）
            //P.286以降を参考にする（ファイル名：setting.xml）

            //自分の回答
            //using (var reader = XmlReader.Create("setting.xml")) {
            //    var serializer = new XmlSerializer(typeof(Settings));
            //    var settings = serializer.Deserialize(reader) as Settings;
            //    BackColor = Color.FromArgb(settings.MainFormBackColor);
            //}

            //模範解答
            if (File.Exists("setting.xml")) {
                try {
                    using (var reader = XmlReader.Create("setting.xml")) {
                        var serializer = new XmlSerializer(typeof(Settings));
                        settings = serializer.Deserialize(reader) as Settings;
                        //背景色設定
                        BackColor = Color.FromArgb(settings.MainFormBackColor);
                        //設定クラスのインスタンスにも現在の設定色を設定
                        settings.MainFormBackColor = BackColor.ToArgb();
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "設定ファイル読み込みエラー";
                    MessageBox.Show(ex.Message); //←より具体的なエラーを出力
                }
            } else {
                tsslbMessage.Text = "設定ファイルがありません";
            }
        }

        private void tsmiExit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        //このアプリについてを選択したときのイベントハンドラ
        private void tsmiAbout_Click(object sender, EventArgs e) {
            fmVersion fmv = new fmVersion();
            fmv.ShowDialog();
        }

        private void 色設定ToolStripMenuItem_Click(object sender, EventArgs e) {
            if (cdColor.ShowDialog() == DialogResult.OK) {
                BackColor = cdColor.Color;
                //設定ファイルへ保存
                settings.MainFormBackColor = cdColor.Color.ToArgb();  //背景色を設定インスタンスへ設定
            }
        }

        //ファイルオープン処理
        private void reportOpenFile() {
            if (ofdReportFileOpen.ShowDialog() == DialogResult.OK) {
                try {
                    //逆シリアル化でバイナリ形式を取り込む
#pragma warning disable SYSLIB0011
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011
                    using (FileStream fs = File.Open(
                        ofdReportFileOpen.FileName, FileMode.Open, FileAccess.Read)) {

                        listCarReports = (BindingList<CarReport>)bf.Deserialize(fs);
                        dgvRecord.DataSource = listCarReports;

                        cbAuthor.Items.Clear();
                        cbCarName.Items.Clear();
                        //コンボボックスへ登録
                        foreach (var report in listCarReports) {
                            setObAuthor(report.Author);
                            setCbCarName(report.CarName);
                        }
                        //自分の答え
                        //setObAuthor(cbAuthor.Text);
                        //setCbCarName(cbCarName.Text);
                    }
                }
                catch (Exception) {
                    tsslbMessage.Text = "ファイル形式が違います";

                }
            }
        }

        //ファイルセーブ処理
        private void reportSaveFile() {
            if (sfdReportFileSave.ShowDialog() == DialogResult.OK) {
                try {
                    //バイナリ形式でシリアル化
#pragma warning disable SYSLIB0011
                    var bf = new BinaryFormatter();
#pragma warning restore SYSLIB0011

                    using (FileStream fs = File.Open(
                                    sfdReportFileSave.FileName, FileMode.Create)) {

                        bf.Serialize(fs, listCarReports);
                    }
                }
                catch (Exception ex) {
                    tsslbMessage.Text = "ファイル書き出しエラー";
                    MessageBox.Show(ex.Message); //←より具体的なエラーを出力
                }
            }
        }

        private void 保存ToolStripMenuItem_Click(object sender, EventArgs e) {
            reportSaveFile(); //ファイルセーブ処理
        }

        private void 開くToolStripMenuItem_Click(object sender, EventArgs e) {
            reportOpenFile(); //ファイルオープン処理
        }

        //フォームが閉じたら呼ばれる
        private void Form1_FormClosed(object sender, FormClosedEventArgs e) {
            //設定ファイルへ色情報を保存する処理（シリアル化）
            //P.284以降を参考にする(ファイル名：setting.xml)

            //自分の回答
            //using (var writer = XmlWriter.Create("setting.xml")) {
            //    var serializer = new XmlSerializer(settings.GetType());
            //    serializer.Serialize(writer, settings);
            //}

            //模範解答
            try {
                using (var writer = XmlWriter.Create("setting.xml")) {
                    var serializer = new XmlSerializer(settings.GetType());
                    serializer.Serialize(writer, settings);
                }
            }
            catch (Exception ex) {
                tsslbMessage.Text = "設定ファイル書き出しエラー";
                MessageBox.Show(ex.Message); //←より具体的なエラーを出力
            }
        }
    }
}
