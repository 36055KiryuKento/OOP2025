using Microsoft.Win32;
using SQLite;
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

namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Person> _persons = new List<Person>();
    
    public MainWindow() {
        InitializeComponent();
        ReadDatabase();
    }

    //画像ファイルをバイト配列に変換するメソッド
    private byte[] ImageToByteArray(string imagePath) {
        return System.IO.File.ReadAllBytes(imagePath);
    }

    private void ReadDatabase() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();

            _persons = connection.Table<Person>()
                        .OrderBy(p => p.Name)
                        .ToList();
        }
        PersonListView.ItemsSource = _persons;
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
            connection.CreateTable<Person>();

            var selectedPerson = PersonListView.SelectedItem as Person;
            if (selectedPerson is null) return;

            var person = new Person() {
                Id = selectedPerson.Id,
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                address = addressTextBox.Text,
                
            };

            connection.Update(person);

            ReadDatabase();
            PersonListView.ItemsSource = _persons;
        }
    }
    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        var person = new Person() {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
            address = addressTextBox.Text,


            
        };

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();
            connection.Insert(person);
        }
        ReadDatabase(); // 保存後リスト更新
    }


    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var item = PersonListView.SelectedItem as Person;
        if (item == null) {
            MessageBox.Show("行を選択してください");
            return;
        }

        //データベース接続
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Person>();
            connection.Delete(item);  //データベースから選択されているレコードの削除
            ReadDatabase();
            PersonListView.ItemsSource = _persons;
        }
    }

    //リストビューのフィルタリング
    private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e) {

        var filterList = _persons.Where(p => p.Name.Contains(SearchTextBox.Text));

        PersonListView.ItemsSource = filterList;
    }
    //リストビューから1レコード選択
    private void PersonListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selectedPerson = PersonListView.SelectedItem as Person;
        if (selectedPerson is null) return;
        NameTextBox.Text = selectedPerson.Name;
        PhoneTextBox.Text = selectedPerson.Phone;
        addressTextBox.Text = selectedPerson.address;
    }
}
