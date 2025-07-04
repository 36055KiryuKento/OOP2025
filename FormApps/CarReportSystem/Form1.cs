using System.ComponentModel;
using System.Drawing.Text;
using static CarReportSystem.CarReport;

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

        //�L�^�҂̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbAuthor(string autor) {
            if (!cbAuthor.Items.Contains(autor)) {
                //���o�^�Ȃ�o�^[�o�^�ς݂Ȃ牽�����Ȃ�]
                cbAuthor.Items.Add(autor);
            }


        }

        //�Ԗ��̗������R���{�{�b�N�X�֓o�^�i�d���Ȃ��j
        private void setCbCarName(string carName) {
            if (!cbCarName.Items.Contains(carName)) {
                //���o�^�Ȃ�o�^[�o�^�ς݂Ȃ牽�����Ȃ�]
                cbCarName.Items.Add(carName);
            }
        }


        private void btRecordAdd_Click(object sender, EventArgs e) {
            var carReport = new CarReport {
                Author = cbAuthor.Text,
                CarName = cbCarName.Text,
                Report = tbReport.Text,
                Picture = pbPicture.Image,
                Date = dtpDate.Value,
                Maker = GetRadioButtonMaker(),

            };
            listCarReports.Add(carReport);
            setCbAuthor(cbAuthor.Text);//�R���{�{�b�N�X�֓o�^
            setCbCarName(cbCarName.Text);
            InputItemAllClear(); //�o�^��͍��ڂ��N���A
        }

        //���͍��ڂ�S�ăN���A
        private void InputItemAllClear() {
            cbAuthor.Text = string.Empty;
            cbCarName.Text = string.Empty;
            tbReport.Text = string.Empty;
            pbPicture.Image = null;
            dtpDate.Value = DateTime.Today;

        }
        private CarReport.MakerGroup GetRadioButtonMaker() {
            if (rbToyota.Checked)
                return MakerGroup.�g���^;
            if (rbNissan.Checked)
                return MakerGroup.���Y;
            if (rbHonda.Checked)
                return MakerGroup.�z���_;
            if (rbSubaru.Checked)
                return MakerGroup.�X�o��;
            if (rbImport.Checked)
                return MakerGroup.�A����;

            return MakerGroup.���̑�;

        }

        private void dgvRecord_Click(object sender, EventArgs e) {

            dtpDate.Value = (DateTime)dgvRecord.CurrentRow.Cells["Date"].Value;
            cbAuthor.Text = (string)dgvRecord.CurrentRow.Cells["Author"].Value;
            cbCarName.Text = (string)dgvRecord.CurrentRow.Cells["CarName"].Value;
            tbReport.Text = (string)dgvRecord.CurrentRow.Cells["Report"].Value;
            pbPicture.Image = (Image)dgvRecord.CurrentRow.Cells["Picture"].Value;
            setRadioButtonMaker((MakerGroup)dgvRecord.CurrentRow.Cells["Maker"].Value);

        }
        //�w�肵�����[�J�[�̃��W�I�{�^�����Z�b�g
        private void setRadioButtonMaker(MakerGroup targetMaker) {
            switch (targetMaker) {

                case MakerGroup.�g���^:
                    rbToyota.Checked = true;
                    break;
                case MakerGroup.���Y:
                    rbNissan.Checked = true;
                    break;
                case MakerGroup.�z���_:
                    rbHonda.Checked = true;
                    break;
                case MakerGroup.�X�o��:
                    rbSubaru.Checked = true;
                    break;
                case MakerGroup.�A����:
                    rbImport.Checked = true;
                    break;
                default:
                    rbOther.Checked = true;
                    break;
            }
        }
        //�V�K�ǉ��̃C�x���g�n���h��
        private void btNewRecord_Click(object sender, EventArgs e) {
            InputItemAllClear();
        }

        //�C���{�^���̃C�x���g�n���h��
        private void btRecordModify_Click(object sender, EventArgs e) {
            int index = dgvRecord.CurrentRow.Index;

            listCarReports[index].Author = cbAuthor.Text;
            listCarReports[index].Date = dtpDate.Value;
            listCarReports[index].CarName = cbCarName.Text;
            listCarReports[index].Report = tbReport.Text;
            listCarReports[index].Picture = pbPicture.Image;
            listCarReports[index].Maker = GetRadioButtonMaker();





            dgvRecord.Refresh();//�f�[�^�O���b�h�r���[�̍X�V
        }

        //�폜�{�^���̃C�x���g�n���h��
        private void btRecordDelete_Click(object sender, EventArgs e) {
           
            //���ݑI������Ă���s�����݂��Ȃ��ꍇ�́A����ȏ㏈�������Ȃ��ł��̃��\�b�h���I������
            if (dgvRecord.CurrentRow == null) return;
            //���ݑI������Ă���s�̔ԍ���index�ɑ��
            int index = dgvRecord.CurrentRow.Index;
            //�w�肵���C���f�b�N�X�ɂ���v�f���폜
            listCarReports.RemoveAt(index);

            InputItemAllClear();

        }

        private void Form1_Load(object sender, EventArgs e) {
            InputItemAllClear();
        }
    }
}

