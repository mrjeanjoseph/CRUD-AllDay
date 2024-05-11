namespace GettingStarted {
    internal class Program {
        static void Main(string[] args) {

            string title = "C-Sharp";

            
            Console.WriteLine(Greetings(title));

            Console.ReadLine();
        }

        private static string Greetings(string str)
        {
            string greeting = "Hello fellow programmers. Let's get ready to";
            return $" {greeting} {str}!";
        }
    }
}
