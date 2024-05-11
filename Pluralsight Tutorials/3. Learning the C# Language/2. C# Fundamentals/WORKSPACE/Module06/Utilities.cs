using System.Globalization;

namespace Module6 {
    internal class Utilities {

        public static void UsingSimpleStrings() {
            string firstName = "Denzel";
            string lastName = "Jean-Joseph";
            string s;
            s = firstName;
            var userName = "rhjeanjoseph";
            userName = userName.ToLower();

            userName = string.Empty;
            userName = "";//identical to string.Empty;
        }


        public static void ManipulatingStrings() {
            string firstName = "Elvila";
            string lastName = "Jean-Joseph";


            string fullName = firstName + " " + lastName;
            string employeeIdentification = String.Concat(firstName, lastName);
            string empId1 = firstName.ToLower().Substring(0,1) + "-" + firstName.ToLower();
            string empId2 = firstName.ToLower() + "-" + lastName.Trim().ToLower();
            int length = empId1.Length;


            if (fullName.Contains("Jean") || fullName.Contains("Jean")) {
                Console.WriteLine("It's a Jean-Joseph Family Member!");
            }
            string subString = fullName.Substring(1, 3);
            Console.WriteLine("Characters 2 to 4 of fullName are " + subString);

            //string interpolation
            string nameUsingInterpolation = $"{firstName}-{lastName}";
            Console.WriteLine(nameUsingInterpolation);
            //combined with implicit typing
            var v = $"Hello, {firstName}!";
            Console.WriteLine(v);
        }

        public static void UsingEscapeCharacters() {
            string firstName = "Raoul";
            string lastName = "Jean-Joseph";
            string displayName = $"Welcome!\n{firstName}\t{lastName}";

            string filePath = "C:\\data\\employeelist.xlsx";
            string marketingTagLine = "Baking the \"best pies\" ever";

            string verbatimFilePath = @"C:\data\employeelist.xlsx";

            Console.WriteLine(filePath);
        }

        public static void UsingStringEquality() {

            string name1 = "Elanie";
            string name2 = "Jean-Joseph";

            Console.WriteLine("Are both names equal? " + (name1 == name2));
            Console.WriteLine("Is name equal to Jean? " + (name1 == "Jean"));
            Console.WriteLine("Is name equal to JEAN? " + name2.Equals("JEAN"));
            Console.WriteLine("Is uppercase name equal to Jean? " + (name1.ToLower() == "Jean"));
            Console.WriteLine("Is uppercase name equal to Jean? " + (name1.Equals("Jean", 
                StringComparison.CurrentCultureIgnoreCase)));
        }

        public static void ParsingStrings() {
            Console.Write("Enter the wage: ");
            string wage = Console.ReadLine();

            //int wageValue = int.Parse(wage);

            double wageValue;
            if (double.TryParse(wage, out wageValue))
                Console.WriteLine("Parsing success: " + wageValue);
            else
                Console.WriteLine("Parsing failed");

            string hireDateString = "12/12/2020";
            DateTime hireDate = DateTime.Parse(hireDateString);
            Console.WriteLine("Parsed date: " + hireDate);
            //TryParse also exists for dates

            var cultureInfo = new CultureInfo("nl-BE");
            string birthDateString = "28 Maart 1984";//Dutch, spoken in Belgium
            var birthDate = DateTime.Parse(birthDateString, cultureInfo);
            Console.WriteLine("Birth date: " + birthDate);


        }    
    }
}
