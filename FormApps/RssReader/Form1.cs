using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RssReader {
    public partial class Form1 : Form {

        private List<ItemData> items;

        Dictionary<string, string> rssUrlDict = new Dictionary<string, string>() {
            {"��v","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"�o��","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"����","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"�G���^��","https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            {"�Ȋw","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"����","https://news.yahoo.co.jp/rss/topics/world.xml" },
            {"�X�|�[�c","https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"�n��","https://news.yahoo.co.jp/rss/topics/local.xml" },

        };

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            {
                // ComboBox�J�e�S����ǉ�
                cbUrl.Items.AddRange(rssUrlDict.Keys.ToArray());

            }
        }
        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(cbUrl.Text);
                XDocument xdoc = XDocument.Parse(xml);

                //RSS����͂��ĕK�v�ȗv�f���擾
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();


                //���X�g�{�b�N�X�փ^�C�g����\��
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "�f�[�^�Ȃ�"));

            }


        }

        //�^�C�g����I���i�N���b�N�j�����Ƃ��ɌĂ΂��C�x���g�n���h��
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);




        }


        private void btMove_Click(object sender, EventArgs e) {
            label1.Visible = false;
            if (wvRssLink.CanGoForward) {
                wvRssLink.GoForward();
            } else {
                lblStatus.Text = "����ȏ�i�߂܂���B";
                lblStatus.Visible = true;   // ���b�Z�[�W�\��
            }
        }


        private void btReturn_Click(object sender, EventArgs e) {
            lblStatus.Visible = false;  // ���b�Z�[�W��\��
            if (wvRssLink.CanGoBack) {
                wvRssLink.GoBack();
            } else {
                label1.Text = "����ȏ�߂�܂���B";
                label1.Visible = true;



            }



        }

     
        }
    }











