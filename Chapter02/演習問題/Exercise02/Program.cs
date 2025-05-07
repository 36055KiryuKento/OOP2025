using DistanceConverter;

namespace Exercise02 {
    internal class Program {
        static void Main(string[] args) {




            Console.Write("はじめ：");  //コンソール入力
            Console.Write("おわり：");

            int start = int.Parse(Console.ReadLine());


            if (args.Length >= 1 && args[0] == "-tom") {
                PrintInchToMeterList(1, 10);
            } else {
                PrintMeterToInchList(1, 10);
            }
        }
        static void PrintInchToMeterList(int start, int end) {
            for (int inch = start; inch <= end; inch++) {
                double meter = InchConverter.ToMeter(inch);
                Console.WriteLine($"{inch}inch = {meter:0.0000}m");
            }
        }

                static void PrintMeterToInchList(int start, int end) {
                    for (int meter = start; meter <= end; meter++) {
                        double inch = InchConverter.ToMeter(meter);
                        Console.WriteLine($"{meter}meter = {inch:0.0000}m");

                    }



                }
            }
        }
   
