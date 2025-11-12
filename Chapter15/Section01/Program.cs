namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            List<IGreeting> greetings = [
                new GreetingMorning(),
                new GreetingAfternoon(),
                new GreetingEvening(),
            ];

            foreach (var obj in greetings) {
                string msg = obj.GetMessage();
                Console.WriteLine(msg);
            }
        }
    }

    class GreetingMorning : IGreeting {
        public string GetMessage() => "おはよう";
    }

    class GreetingAfternoon : IGreeting {
        public string GetMessage() => "こんにちわ";
    }

    class GreetingEvening : IGreeting {
        public string GetMessage() => "こんばんわ";
    }
}

