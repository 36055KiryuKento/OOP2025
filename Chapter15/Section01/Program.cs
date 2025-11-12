namespace Section01 {
    internal class Program {
        static void Main(string[] args) {
            List<GreetingBase> list = [
                new GreetingMorning(),
               new GreetingAfternoon(),
               new GreetingEvening(),
               ];

            foreach (var obj in list) {
                string msg = obj.GetMessarge();
                Console.WriteLine(msg);
            }
        }

        class GreetingMorning : GreetingBase {
            public override string GetMessarge() => "おはよう";
        }

        class GreetingAfternoon : GreetingBase {
            public override string GetMessarge() => "こんにちわ";
        }

        class GreetingEvening : GreetingBase {
            public override string GetMessarge() => "こんばんわ";
        }
    }
}
