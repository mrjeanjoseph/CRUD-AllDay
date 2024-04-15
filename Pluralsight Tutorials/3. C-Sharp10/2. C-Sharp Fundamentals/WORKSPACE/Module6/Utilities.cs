using System.Globalization;

namespace Module6 {
    public class Utilities {

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
            //string empId = firstName.ToLower() + "-" + firstName.ToLower();
            string empId = firstName.ToLower() + "-" + lastName.Trim().ToLower();
            int length = empId.Length;


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

        public static int CalculateYearlyWage(int monthlyWage, int numberOfMonthsWorked) {
            //Console.WriteLine($"The yearly wage is: {monthlyWage * numberOfMonthsWorked}");
            //return monthlyWage * numberOfMonthsWorked;

            if (numberOfMonthsWorked == 12)//let's add a bonus month
                return monthlyWage * (numberOfMonthsWorked + 1);

            return monthlyWage * numberOfMonthsWorked;
        }

        public static int CalculateYearlyWage(int monthlyWage, int numberOfMonthsWorked, int bonus) {
            Console.WriteLine($"The yearly wage is: {monthlyWage * numberOfMonthsWorked + bonus}");
            return monthlyWage * numberOfMonthsWorked + bonus;
        }

        public static double CalculateYearlyWage(double monthlyWage, double numberOfMonthsWorked, double bonus) {
            Console.WriteLine($"The yearly wage is: {monthlyWage * numberOfMonthsWorked + bonus}");
            return monthlyWage * numberOfMonthsWorked + bonus;
        }

        public static void UsingOptionalParameters() {
            int monthlyWage1 = 1234;
            int months1 = 12;

            int yearlyWageForEmployee1 = CalculateYearlyWageWithOptional(monthlyWage1, months1);
            Console.WriteLine($"Yearly wage for employee 1 (Bethany): {yearlyWageForEmployee1}");
        }

        public static int CalculateYearlyWageWithOptional(int monthlyWage, int numberOfMonthsWorked, int bonus = 0) {

            Console.WriteLine($"The yearly wage is: {monthlyWage * numberOfMonthsWorked + bonus}");
            return monthlyWage * numberOfMonthsWorked + bonus;
        }


        public static void UsingNamedArguments() {
            int amount = 1234;
            int months = 12;
            int bonus = 500;

            int yearlyWageForEmployee1 = CalculateYearlyWageWithNamed(bonus: bonus, numberOfMonthsWorked: months, monthlyWage: amount);
            Console.WriteLine($"Yearly wage for employee 1 (Bethany): {yearlyWageForEmployee1}");
        }

        public static int CalculateYearlyWageWithNamed(int monthlyWage, int numberOfMonthsWorked, int bonus) {

            Console.WriteLine($"The yearly wage is: {monthlyWage * numberOfMonthsWorked + bonus}");
            return monthlyWage * numberOfMonthsWorked + bonus;
        }
    
    }
}
