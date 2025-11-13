using TextFileProcessor;

namespace LineCounter {
    internal class Program {
        static void Main(string[] args) {
            TextProcessor.Run<LineCounterProcessor>(args[0]);
            Console.Write("入力してください");
             Console.ReadLine();
        }
    }
}
