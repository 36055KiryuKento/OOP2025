
using System.Security.Cryptography.X509Certificates;

namespace Exercise01 {
    internal class Program {
        static void Main(string[] args) {
            var text = "Cozy lummox gives smart squid who asks for job pen";

            Exercise1(text);
            Console.WriteLine();

            Exercise2(text);

        }


        private static void Exercise1(string text) {
            var dict = new Dictionary<Char, int>();

            // A〜Z をキーとして初期化
            for (var ch = 'A'; ch <= 'Z'; ch++) {
                dict[ch] = 0;

            }
            // テキストを大文字にしてループ
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    dict[c]++;

                }
            }
            // 並び変えて結果を出力
            foreach (var s in dict) {
                Console.WriteLine($"{s.Key}:{s.Value}");
            }

        }

        private static void Exercise2(string text) {
            var dict = new SortedDictionary<Char, int>();

            // A〜Z をキーとして初期化
            for (var ch = 'A'; ch <= 'Z'; ch++) {
                dict[ch] = 0;

            }
            // テキストを大文字にしてループ
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    dict[c]++;

                }
            }
            // 結果を出力
            foreach (var s in dict) {
                Console.WriteLine($"{s.Key}:{s.Value}");
            }

        }
    }
    }




