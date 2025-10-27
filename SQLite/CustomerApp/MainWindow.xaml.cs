using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomerApp {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        OpenFileDialog ofd;
        private List<Customer> _customer = new List<Customer>();

        public MainWindow() {
            InitializeComponent();
            ReadDatabase();
        }

        private void ReadDatabase() {
            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();

                _customer = connection.Table<Customer>()
                            .OrderBy(p => p.Name)
                            .ToList();
            }
            PersonListView.ItemsSource = _customer;
        }
        private void ImageButton_Click(object sender, RoutedEventArgs e) {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "画像ファイル (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg";

            bool? result = dlg.ShowDialog();

            if (result == true) {

                string filename = dlg.FileName;


                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filename);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();

                SelectedImage.Source = bitmap;

            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e) {

            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();

                var selectedPerson = PersonListView.SelectedItem as Customer;
                if (selectedPerson is null) return;

                var person = new Customer() {
                    Id = selectedPerson.Id,
                    Name = NameTextBox.Text,
                    Phone = PhoneTextBox.Text,
                    Address = addressTextBox.Text,

                };

                connection.Update(person);

                ReadDatabase();
                PersonListView.ItemsSource = _customer;
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e) {

            var person = new Customer() {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = addressTextBox.Text,

                Picture = ImageSourceToByteArray(SelectedImage.Source)


            };

            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();
                connection.Insert(person);
            }
            ReadDatabase(); // 保存後リスト更新
            PersonListView.ItemsSource = _customer;

        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var item = PersonListView.SelectedItem as Customer;
            if (item == null) {
                MessageBox.Show("行を選択してください");
                return;
            }

            //データベース接続
            using (var connection = new SQLiteConnection(App.databasePath)) {
                connection.CreateTable<Customer>();
                connection.Delete(item);  //データベースから選択されているレコードの削除
                ReadDatabase();
                PersonListView.ItemsSource = _customer;
            }
        }


        //リストビューのフィルタリング
        private void SearchTextBox_TextChanged_1(object sender, TextChangedEventArgs e) {
            var filterList = _customer.Where(p => p.Name.Contains(SearchTextBox.Text) ||
            p.Phone.ToLower().Contains(SearchTextBox.Text) ||
            p.Address.ToLower().Contains(SearchTextBox.Text));

            PersonListView.ItemsSource = filterList;
        }
        //リストビューから1レコード選択
        private void PersonListView_SelectionChanged_1(object sender, SelectionChangedEventArgs e) {
            var selectedPerson = PersonListView.SelectedItem as Customer;
            if (selectedPerson is null) return;
            NameTextBox.Text = selectedPerson.Name;
            PhoneTextBox.Text = selectedPerson.Phone;
            addressTextBox.Text = selectedPerson.Address;

            

            if (selectedPerson.Picture != null && selectedPerson.Picture.Length > 0) {
                BitmapImage bitmap = new BitmapImage();
                using (var ms = new System.IO.MemoryStream(selectedPerson.Picture)) {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = ms;
                    bitmap.EndInit();

                }
                SelectedImage.Source = bitmap;
            } else {
                SelectedImage.Source = null;  // 写真がなければ画像クリア
            }
        }

        public static byte[] ImageSourceToByteArray(ImageSource imageSource) {
            if (imageSource == null) {
                return null;
            }

            byte[] byteArray = null;
            // MemoryStreamを作成
            using (var stream = new MemoryStream()) {
                // PngEncoderを作成
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)imageSource));
                // MemoryStreamにエンコードを保存
                encoder.Save(stream);
                // MemoryStreamの内容をbyte配列として取得
                byteArray = stream.ToArray();
            }
            return byteArray;
        }

        public static BitmapImage byteToBitmap(byte[] bytes) {
            var result = new BitmapImage();

            using (var stream = new MemoryStream(bytes)) {
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.CreateOptions = BitmapCreateOptions.None;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();    // 非UIスレッドから作成する場合、Freezeしないとメモリリークするため注意
            }
            return result;
        }
    }
}


