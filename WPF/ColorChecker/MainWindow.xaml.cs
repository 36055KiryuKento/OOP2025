using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ColorChecker {
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window {
        public MyColor[] ColorsList { get; set; }

        public MainWindow() {
            InitializeComponent();

            // 色リストを作成しプロパティにセット
            ColorsList = GetColorList();

            // DataContextを自身に設定することで、XAMLのバインディングが機能する
            this.DataContext = this;

            // 初期表示用に色を設定しておくと良い
            if (ColorsList.Length > 0) {
                setSliderValue(ColorsList[0].Color);
            }
        }

        // 既存のGetColorListメソッド
        private MyColor[] GetColorList() {
            return typeof(Colors).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Select(p => new MyColor() { Color = (Color)p.GetValue(null), Name = p.Name }).ToArray();
        }

        // (その他のコードはそのままでOK)

        //全てのスライダーから呼ばれるイベントハンドラ
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
            {
                var myColor = new MyColor {
                    Color = Color.FromRgb((byte)rSlider.Value, (byte)gSlider.Value, (byte)bSlider.Value),
                    Name = string.Empty
                };

                // 背景を更新
                colorArea.Background = new SolidColorBrush(myColor.Color);

                // スライダーの値から現在の色を取得
                var currentColor = myColor.Color;

                // ColorsListコンボボックスの一覧から一致する色を探す
                var matchedColor = ColorsList.FirstOrDefault(c =>
                    c.Color.R == currentColor.R &&
                    c.Color.G == currentColor.G &&
                    c.Color.B == currentColor.B);

                if (matchedColor != null) {
                    // 一致したら ComboBox の選択を変更
                    colorSelectComboBox.SelectedItem = matchedColor;
                } else {
                    // 一致しなければ選択解除
                    colorSelectComboBox.SelectedItem = null;
                }
            }
        }

        private void StockButton_Click(object sender, RoutedEventArgs e) {

            byte r = (byte)rSlider.Value;
            byte g = (byte)gSlider.Value;
            byte b = (byte)bSlider.Value;


            // 現在の色を作成
            Color currentColor = Color.FromRgb(r, g, b);

            // ColorsList に一致する色があるか探す
            var matchedColor = ColorsList.FirstOrDefault(c => c.Color.R == currentColor.R &&
                                                               c.Color.G == currentColor.G &&
                                                               c.Color.B == currentColor.B);

            // 見つかったら名前を追加
            if (matchedColor != null && !string.IsNullOrEmpty(matchedColor.Name)) {
                colorList.Items.Add(matchedColor.Name);
            } else {
                string colorText = $"R: {r}  G: {g}  B: {b}";
                colorList.Items.Add(colorText);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            // ComboBox の選択が変更されたときの処理をここに記述します。
            var comboBox = (ComboBox)sender;
            if (comboBox.SelectedItem is MyColor selectedColor) {
                setSliderValue(selectedColor.Color);
            }
        }
        /// <summary>
        /// すべての色を取得するメソッド
        /// </summary>
        /// <returns></returns>

            // private void colorSelectComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            //  if (((ComboBox)sender).SelectedIndex = -1) return;


            //var comboSelectMyColor = (MyColor)((ComboBox)sender).SelectedItem;
            // setSliderValue(comboSelectMyColor.Color);//スライダーを設定

            //}
        private void setSliderValue(Color color) {
            rSlider.Value = color.R;
            gSlider.Value = color.G;
            bSlider.Value = color.B;
        }

    }
}


