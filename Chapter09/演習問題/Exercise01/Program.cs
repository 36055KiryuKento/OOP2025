using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var dateTime = DateTime.Now;
            DisplayDateParttern1(dateTime);
            DisplayDateParttern2(dateTime);
            DisplayDateParttern3(dateTime);
        }

        private static void DisplayDateParttern1(DateTime dateTime) {
            //2024/03/09 19:03
            //string.Formatを使った例
            var str = string.Format($"{dateTime:yyyy/MM/dd HH:mm}");
            Console.WriteLine(str);

        }

        private static void DisplayDateParttern2(DateTime dateTime) {
            //2024年03月09日　19時03分09秒
            //DateTime.Tostringを使った例
            var str = dateTime.ToString("yyyy年MM月dd日　HH時mm分ss秒");
            Console.WriteLine(str);

        }

        private static void DisplayDateParttern3(DateTime dateTime) {
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();

            //和暦2桁表示（ゼロサプレスなし）
            var datestr = dateTime.ToString("ggyy", culture);
            var dayOfWeek = culture.DateTimeFormat.GetDayName(dateTime.DayOfWeek);

            var str = string.Format($"{datestr}年{dateTime.Month,2}月{dateTime.Day}日({dayOfWeek})");
            Console.WriteLine(str);

            //和暦2桁表示（ゼロサプレスあり）
            var cul = dateTime.ToString("gg", culture);
            var year = int.Parse(dateTime.ToString("yy", culture));
            var str2 = string.Format($"{cul}{year,2}年{dateTime.Month,2}月{dateTime.Day,2}日({dayOfWeek})");
            Console.WriteLine(str2);

        }
    }
}
