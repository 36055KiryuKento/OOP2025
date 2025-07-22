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
            {"主要","https://news.yahoo.co.jp/rss/topics/top-picks.xml" },
            {"経済","https://news.yahoo.co.jp/rss/topics/business.xml" },
            {"IT","https://news.yahoo.co.jp/rss/topics/it.xml" },
            {"国内","https://news.yahoo.co.jp/rss/topics/domestic.xml" },
            {"エンタメ","https://news.yahoo.co.jp/rss/topics/entertainment.xml" },
            {"科学","https://news.yahoo.co.jp/rss/topics/science.xml" },
            {"国際","https://news.yahoo.co.jp/rss/topics/world.xml" },
            {"スポーツ","https://news.yahoo.co.jp/rss/topics/sports.xml" },
            {"地域","https://news.yahoo.co.jp/rss/topics/local.xml" },

        };

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            {
                // ComboBoxカテゴリを追加
                cbUrl.Items.AddRange(rssUrlDict.Keys.ToArray());

            }
        }
        private async void btRssGet_Click(object sender, EventArgs e) {

            using (var hc = new HttpClient()) {

                string xml = await hc.GetStringAsync(cbUrl.Text);
                XDocument xdoc = XDocument.Parse(xml);

                //RSSを解析して必要な要素を取得
                items = xdoc.Root.Descendants("item")
                    .Select(x =>
                        new ItemData {
                            Title = (string?)x.Element("title"),
                            Link = (string?)x.Element("link"),
                        }).ToList();


                //リストボックスへタイトルを表示
                lbTitles.Items.Clear();
                items.ForEach(item => lbTitles.Items.Add(item.Title ?? "データなし"));

            }


        }

        //タイトルを選択（クリック）したときに呼ばれるイベントハンドラ
        private void lbTitles_Click(object sender, EventArgs e) {
            wvRssLink.Source = new Uri(items[lbTitles.SelectedIndex].Link);




        }


        private void btMove_Click(object sender, EventArgs e) {
            label1.Visible = false;
            if (wvRssLink.CanGoForward) {
                wvRssLink.GoForward();
            } else {
                lblStatus.Text = "これ以上進めません。";
                lblStatus.Visible = true;   // メッセージ表示
            }
        }


        private void btReturn_Click(object sender, EventArgs e) {
            lblStatus.Visible = false;  // メッセージ非表示
            if (wvRssLink.CanGoBack) {
                wvRssLink.GoBack();
            } else {
                label1.Text = "これ以上戻れません。";
                label1.Visible = true;



            }



        }

     
        }
    }











