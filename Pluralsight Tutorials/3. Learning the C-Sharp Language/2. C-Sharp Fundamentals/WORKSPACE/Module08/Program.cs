using System.Text;

namespace ValueAndRefTypes {
    public static class Program {
        static void Main(string[] args) {

            Console.WriteLine("Understanding Value and Reference Types:");
            Console.WriteLine("--------------------\n");

            //StringValueAndReference();
            //StringImmunibility();
            //HowStringUseMemory();
            //UseStringBuilderInstead();
            //HowStringBuilderUseMemory();
            //ReturnEmployeeAsJson();
            //UsingEnumTypes();
            UsingStructType();
            Console.ReadLine();
        }

        public static void UsingStructType() {
            WorkTask tasks = new() {
                description = "buy a new eighteen wheeler",
                hours = 32
            };
            tasks.PerformWorkTask();
        }

        public static void UsingEnumTypes() {
            Employee employeeOne =
                new Employee("Casteeleaux", "Jean-Joseph", "cjeanjoseph@dvc.com", new DateTime(2017, 4, 23), 29, EmployeeType.Manager);
            Employee employeeTwo =
                new Employee("Ethan", "Jean-Joseph", "etjeanjoseph@dvc.com", new DateTime(2012, 1, 31), 29, EmployeeType.Sales);
            employeeOne.DisplayEmployeeDetails();
            employeeOne.ReceiveWage(true);
            employeeTwo.ReceiveWage(true);

            employeeOne.PerformWork(100);
            int minBonus = 250;
            int receivedBonus = employeeOne.CalculateBonus(minBonus);
            Console.WriteLine($"The minimum bonus is {minBonus} and {employeeOne.firstName} has received a bonus of {receivedBonus}\n\n");
        }

        public static void UsingCustomTypes() {
            List<string> listOfStr = new List<string>();

            StringBuilder stringBuil = new StringBuilder();
        }

        public static void ReturnEmployeeAsJson() {
            Employee employeeFive = new Employee("Elijah", "Jean-Joseph", "eljeanjoseph@dvc.com", new DateTime(2007, 5, 31), 47);
            Console.WriteLine(employeeFive.ConvertToJson());
        }

        public static void UseStringBuilderInstead() {
            string firstName = "Kervens";
            string lastName = "Jean-Joseph";

            StringBuilder sb = new StringBuilder();

            sb.Append("Last Name: ");
            sb.AppendLine(lastName);
            sb.Append("First Name: ");
            sb.AppendLine(firstName);
            string result = sb.ToString();
            Console.WriteLine(result);


        }

        public static void HowStringUseMemory() {
            string indexes = string.Empty;
            for (int i = 0; i < 5000; i++) {
                //Console.Beep();
                indexes += (i.ToString() + " ");
            }
            Console.WriteLine($"Total: {indexes} memory used up!");
        }
        public static void HowStringBuilderUseMemory() {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5000; i++) {
                //Console.Beep();
                sb.Append(i);
                sb.Append(" ");
            }
            string result = sb.ToString();
            Console.WriteLine($"Total: {result} memory used up!");
        }

        public static void StringImmunibility() {
            string scientistOne = "Casteeleaux";
            string scientistTwo = scientistOne;
            scientistOne += " Jean-Joseph";

            Console.WriteLine($"Scientist One: {scientistOne}");
            Console.WriteLine($"Scientist Two: {scientistTwo}");

            string scientistTwoUpper = scientistTwo.ToUpper();
            Console.WriteLine($"Scientist One: {scientistOne}");
            Console.WriteLine($"Scientist Two: {scientistTwo}");
            Console.WriteLine($"Scientist ToUpper: {scientistTwoUpper}");

        }

        public static void StringValueAndReference() {

            int a = 4200;
            int aCopy = 5000;
            Console.WriteLine($"Value of a: {a} and value of copy of a: {aCopy}");
            aCopy = 19;
            Console.WriteLine($"Value of a: {a} and value of copy of a: {aCopy}");


            Employee employeeOfTheYear = new Employee("Ethan", "Jean-Joseph", "etjeanjoseph@dvc.com", new DateTime(2012, 1, 31), 29);

            Employee newLeadEmployee = employeeOfTheYear;
            newLeadEmployee.firstName = "Denzel";

            employeeOfTheYear.DisplayEmployeeDetails();
            newLeadEmployee.DisplayEmployeeDetails();

            employeeOfTheYear.PerformWork(100);
            int minBonus = 250;
            int receivedBonus = employeeOfTheYear.CalculateBonus(minBonus);
            Console.WriteLine($"The minimum bonus is {minBonus} and {employeeOfTheYear.firstName} has received a bonus of {receivedBonus}\n\n");

            int bonusTax = 0;
            receivedBonus = employeeOfTheYear.CalculateBonusAndBonusTax(minBonus, ref bonusTax);
            Console.WriteLine($"The minimum bonus is {minBonus}, the bonus tax is {bonusTax}, and {employeeOfTheYear.firstName} has received a bonus of {receivedBonus}\n\n");

            int bonusTax2;
            receivedBonus = employeeOfTheYear.CalculateBonusAndBonusTax2(minBonus, out bonusTax2);
            Console.WriteLine($"The minimum bonus is {minBonus}, the bonus tax is {bonusTax2}, and {employeeOfTheYear.firstName} has received a bonus of {receivedBonus}\n\n");

        }
    }
}
