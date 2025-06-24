using Microsoft.VisualBasic;
using System.Globalization;

namespace Section01 {
    internal class Program {
        static void Main(string[] args) {

            var today = new DateTime(2005,7,12);//日付
            var now = DateTime.Now;   //日付と時刻


          
           // Console.WriteLine($"Today:{today}");
            //Console.WriteLine($"Now:{now}");

            //自分の生年月日は何曜日かをプログラムを書いて調べる
            //年を入力
            Console.WriteLine("年：");
            var Year = int.Parse(Console.ReadLine());
            //月を入力
            Console.WriteLine("月：");
            var month = int.Parse(Console.ReadLine());
            //日を入力
            Console.WriteLine("日：");
            var day = int.Parse(Console.ReadLine());

            var birth = new DateTime(Year, month, day);

            //日付を和暦で表示
            var culture = new CultureInfo("ja-JP");
            culture.DateTimeFormat.Calendar = new JapaneseCalendar();
            var str = birth.ToString("ggyy年M月d日", culture);

            //指定した日付の曜日の文字列を得る
            var dayOfWeek = culture.DateTimeFormat.GetDayName(birth.DayOfWeek);

            //指定した日付の元号を得る
           // var era = culture.DateTimeFormat.Calendar.GetEra(birth);
            //var eraName = culture.DateTimeFormat.GetEraName(era);

            Console.WriteLine($"{str}は{dayOfWeek}です");

            //うるう年の判定プログラムを作成する
            //var isLeapYear = DateTime.IsLeapYear(2005);
            //if (isLeapYear) {
           //     Console.WriteLine($"年は閏年です");
          //  }else{
          //      Console.WriteLine($"年は平年です");
           // }








        }
    }
}
