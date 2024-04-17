
namespace ClassesAndObjects {
    internal class Program {
        static void Main(string[] args) {

            Console.WriteLine("Creating employee one:");
            Console.WriteLine("--------------------\n");

            Employee employeeOne = new Employee("Kervens", "Jean-Joseph", "kjeanjoseph@dvc.com", new DateTime(1995, 1,3), 55);

            employeeOne.DisplayEmployeeDetails();
            employeeOne.PerformWork();
            employeeOne.PerformWork();
            employeeOne.PerformWork(7);
            employeeOne.PerformWork();

            double recievedWageEmployeeOne = employeeOne.ReceiveWage(true);
            Console.WriteLine($"Wage paid (Message from Program): {recievedWageEmployeeOne}");

            //Creating a new employee
            Console.WriteLine("\n\nCreating employee two");
            Console.WriteLine("--------------------\n");

            Employee employeeTwo = new Employee("Julien", "Dure", "jdure01@dvc.com", new DateTime(2009, 12, 13), 48);

            employeeTwo.DisplayEmployeeDetails();
            employeeTwo.PerformWork();
            employeeTwo.PerformWork();
            employeeTwo.PerformWork(7);
            employeeTwo.PerformWork();

            double recievedWageEmployeeTwo = employeeTwo.ReceiveWage(true);
            Console.WriteLine($"Wage paid (Message from Program): {recievedWageEmployeeTwo}");


            Console.Clear();
            //Creating a third employee - Manupilate
            Console.WriteLine("\n\nManupilating existing employee");
            Console.WriteLine("--------------------\n");

            employeeTwo.firstName = "Elanie";
            employeeTwo.lastName = "Jean Joseph";
            employeeTwo.standardWage = 51;

            employeeTwo.DisplayEmployeeDetails();
            employeeTwo.PerformWork();
            employeeTwo.PerformWork();
            employeeTwo.PerformWork(7);
            employeeTwo.PerformWork();

            double recievedWageEmployeeUpdated = employeeTwo.ReceiveWage(true);
            Console.WriteLine($"Wage paid (Message from Program): {recievedWageEmployeeUpdated}");


            Console.ReadLine();
        }
    }
}
