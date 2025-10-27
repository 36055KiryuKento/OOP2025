using SQLite;
using System.IO;
using System.Windows.Media.Imaging;

namespace CustomerApp.Data {
    class Customer {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 電話番号
        /// </summary>
        public string Phone { get; set; } = string.Empty;
        /// <summary>
        ///  住所
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// 写真
        /// </summary>
        public byte[] Picture { get; set; }

        // 画像をListViewで表示するためのプロパティ
        [Ignore]  // SQLiteに保存しない
        public BitmapImage PhotoImage {
            get {
                if (Picture == null || Picture.Length == 0)
                    return null;

                using (var ms = new MemoryStream(Picture)) {
                    var bmp = new BitmapImage();
                    bmp.BeginInit();
                    bmp.CacheOption = BitmapCacheOption.OnLoad;
                    bmp.StreamSource = ms;
                    bmp.EndInit();
                    bmp.Freeze(); // メモリリーク防止
                    return bmp;
                }
            }
        }
    }
}
