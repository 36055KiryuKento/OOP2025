using System.Globalization;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("１つめの文字列");
            var str1 = Console.ReadLine();
            Console.WriteLine("2つめの文字列");
            var str2 = Console.ReadLine();

            var cultureinfo = new CultureInfo("ja-JP");
            if (String.Compare(str1, str2, cultureinfo, CompareOptions.None) == 0) {
                Console.WriteLine("等しい");
            } else
                Console.WriteLine("等しくない");
            }
        }
    }

