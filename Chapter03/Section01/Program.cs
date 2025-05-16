namespace Section01 {
    internal class Program {


        static void Main(string[] args) {
            var cities = new List<string> {
    "Tokyo",
    "New Delhi",
    "Bangkok",
    "London",
    "Paris",
    "Berlin",
    "Canberra",
    "Hong Kong",
};

            var loewerList = cities.ConvertAll(s => s.ToUpper());
            loewerList.ForEach(s => Console.WriteLine(s));


             }
        }
    }

