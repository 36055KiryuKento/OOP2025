
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

   
        for (var ch = 'A'; ch <= 'Z'; ch++) {
            dict[ch] = 0;

        }
            foreach (var c in text.ToUpper()) {
                if ('A' <= c && c <= 'Z') {
                    dict[c]++;

                }
            }

                foreach(var s in dict) {
                    Console.WriteLine($"{s.Key}:{s.Value}");
                }

            }

private static void Exercise2(string text) {

}
    }
}