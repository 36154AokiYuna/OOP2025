using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Diagnostics.Metrics;

namespace CarReportSystem {
    public partial class Form1 : Form {
        //�J�[���|�[�g�Ǘ��p���X�g
        BindingList<CarReport> listCarReports = new BindingList<CarReport>();

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
            var carReport = new CarReport {
                Date = dtpDate.Value,
                Author = cbAuthor.Text,
                Maker = GetRadioButtonMaker(),
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
            };
            listCarReports.Add(carReport);
            setObAuthor(cbAuthor.Text); //�R���{�{�b�N�X�֓o�^
            setCbCarName(cbCarName.Text);
            InputItemsAllClear(); //�o�^��͍��ڂ��N���A
        }

        //���͍��ڂ����ׂăN���A
        private void InputItemsAllClear() {
            dtpDate.Value = DateTime.Today;
            cbAuthor.Text = "";
            rbOther.Checked = true;
            cbCarName.Text = "";
            tbReport.Text = "";
            pbPicture.Image = null;
        }

        private CarReport.MakerGroup GetRadioButtonMaker() {
            if (rbToyota.Checked) {
                return CarReport.MakerGroup.�g���^;
            } else if (rbNissan.Checked) {
                return CarReport.MakerGroup.���Y;
            } else if (rbHonda.Checked) {
                return CarReport.MakerGroup.�z���_;
            } else if (rbSubaru.Checked) {
                return CarReport.MakerGroup.�X�o��;
            } else if (rbImport.Checked) {
                return CarReport.MakerGroup.�A����;
            } else if (rbOther.Checked) {
                return CarReport.MakerGroup.���̑�;
            } else {
                return CarReport.MakerGroup.�Ȃ�;
            }
        }

        private void dgvRecord_Click(object sender, EventArgs e) {
            dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
            cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
            setRadioButtonMaker((CarReport.MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);
            cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
            tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
            pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
        }

        //�w�肵�����[�J�[�̃��W�I�{�^�����Z�b�g
        private void setRadioButtonMaker(CarReport.MakerGroup targetMaker) {
            switch (targetMaker) {
                case CarReport.MakerGroup.�g���^:
                    rbToyota.Checked = true;
                    break;
                case CarReport.MakerGroup.���Y:
                    rbNissan.Checked = true;
                    break;
                case CarReport.MakerGroup.�z���_:
                    rbHonda.Checked = true;
                    break;
                case CarReport.MakerGroup.�X�o��:
                    rbSubaru.Checked = true;
                    break;
                case CarReport.MakerGroup.�A����:
                    rbImport.Checked = true;
                    break;
                default:
                    rbOther.Checked = true;
                    break;
            }
        }

        //�L�^�҂̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setObAuthor(string author) {
            //���ɓo�^�ς݂��m�F
            if (!cbAuthor.Items.Contains(author)) {
                //���o�^�Ȃ�o�^�y�o�^�ς݂Ȃ牽�����Ȃ��z
                cbAuthor.Items.Add(author);
            }
        }

        //�Ԗ��̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbCarName(string carName) {
            if (!cbCarName.Items.Contains(carName)) {
                cbCarName.Items.Add(carName);
            }
        }

        //�V�K���͂̃C�x���g�n���h��
        private void btNewRecord_Click(object sender, EventArgs e) {
            InputItemsAllClear();
        }

        //�C���{�^���̃C�x���g�n���h��
        private void btRecordModify_Click(object sender, EventArgs e) {

        }

        //�폜�{�^���̃C�x���g�n���h��
        private void btRecordDelete_Click(object sender, EventArgs e) {
            //�J�[���|�[�g�Ǘ��p���X�g����Y������f�[�^���폜����
            var index = dgvRecord.CurrentRow.Index;
            listCarReports.RemoveAt(index);
            InputItemsAllClear();
        }

        private void Form1_Load(object sender, EventArgs e) {
            InputItemsAllClear();
        }
    }
}
