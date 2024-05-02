using UnderstandingOO.HR;

namespace UnderstandingOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");


            Console.ReadLine();

            BaseAndDerived();
            AddEncapsulation();
        }

        static void BaseAndDerived()
        {
            Employee employeeOne = new Employee("Bethany", "Smith", "bethany@snowball.be", new DateTime(1979, 1, 16), 25);
            Manager managerOne = new Manager("Mary", "Jones", "mary@snowball.be", new DateTime(1965, 1, 16), 30);
            JuniorResearcher bobJunior = new JuniorResearcher("Bob", "Spencer", "bob@snowball.be", new DateTime(1988, 1, 23), 17);

            employeeOne.DisplayEmployeeDetails();
            employeeOne.PerformWork();
            employeeOne.PerformWork(5);
            employeeOne.PerformWork();
            employeeOne.ReceiveWage();

            managerOne.DisplayEmployeeDetails();
            managerOne.PerformWork();
            managerOne.PerformWork(11);
            managerOne.PerformWork();
            managerOne.ReceiveWage();


            managerOne.AttendManagementMeeting();
            bobJunior.ResearchNewPieTastes(5);
            bobJunior.ResearchNewPieTastes(5);
        }

        static void AddEncapsulation()
        {
            Employee employeeOne = new Employee("Bethany", "Smith", "bethany@snowball.be", new DateTime(1979, 1, 16), 25);
            Employee employeeTwo = new("George", "Jones", "george@snowball.be", new DateTime(1984, 3, 28), 30);

            employeeOne.DisplayEmployeeDetails();
            employeeOne.PerformWork(8);
            employeeOne.PerformWork();
            employeeOne.PerformWork(3);
            employeeOne.ReceiveWage();

            //bethany.firstName = "John";
            employeeOne.FirstName = "John";
            employeeOne.DisplayEmployeeDetails();

            //bethany.Wage = 300;
        }
    }
}
