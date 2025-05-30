
namespace Exercise03 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Jackdaws love my big sphinx of quartz";

            Console.WriteLine("6.3.1");
            Exercise1(text);

            Console.WriteLine("6.3.2");
            Exercise2(text);

            Console.WriteLine("6.3.3");
            Exercise3(text);

            Console.WriteLine("6.3.4");
            Exercise4(text);

            Console.WriteLine("6.3.5");
            Exercise5(text);

        }

        private static void Exercise1(string text) {
            var Count = text.Count(c => c == ' ');//Countで空白をカウント
            Console.WriteLine($"空白の文字数: {Count}");
        }

        private static void Exercise2(string text) {
            var target = text.Replace("big", "wish");//Replaceで置き換え
            Console.WriteLine(target);

        }

        private static void Exercise3(string text) {


        }

        private static void Exercise4(string text) {
            var words = text.Split(' ');//Splitで空白で区切る
            foreach (var word in words.Where(w => w.Length >= 1)) { // 1文字以上の単語を抽出
                if()
                Console.WriteLine(word);

            }
        }

        private static void Exercise5(string text) {
            var words = text.Split(' ');//Splitで空白で区切る
            foreach (var word in words.Where(w => w.Length <= 4)) { // 4文字以下の単語を抽出
             Console.WriteLine(word);
            }
        }
    }
}