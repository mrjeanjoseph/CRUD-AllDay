using UnderstandingOO.HR;

namespace UnderstandingOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            UsingPolymorphism();

            Console.ReadLine();

            HasRelationship();

            IsRelationship();

            BaseAndDerived();

            AddEncapsulation();
        }

        static void UsingPolymorphism()
        {

            IEmployee storeMan = new StoreManager("Bethany", "Smith", "bethany@snowball.be", new DateTime(1979, 1, 16), 25);
            IEmployee regManager = new Manager("Mary", "Jones", "mary@snowball.be", new DateTime(1965, 1, 16), 30);
            JuniorResearcher jrResearcher = new JuniorResearcher("Bob", "Spencer", "bob@snowball.be", new DateTime(1988, 1, 23), 17);
            IEmployee storeMan2 = new StoreManager("Kevin", "Marks", "kevin@snowball.be", new DateTime(1953, 12, 12), 10);
            IEmployee storeMan3 = new StoreManager("Kate", "Greggs", "kate@snowball.be", new DateTime(1993, 8, 8), 10);

            storeMan.DisplayEmployeeDetails();
            storeMan.PerformWork();
            storeMan.PerformWork(10);
            storeMan.PerformWork();
            storeMan.ReceiveWage();

            regManager.DisplayEmployeeDetails();
            regManager.PerformWork();
            regManager.PerformWork(3);
            regManager.PerformWork();
            //mary.AttendManagementMeeting();
            regManager.ReceiveWage();

            jrResearcher.ResearchNewPieTastes(5);
            jrResearcher.ReceiveWage();

            List<IEmployee> employees = new List<IEmployee>();
            employees.Add(storeMan);
            employees.Add(regManager);
            employees.Add(jrResearcher);
            employees.Add(storeMan2);
            employees.Add(storeMan3);

            foreach (var employee in employees)
            {
                employee.PerformWork();
                employee.ReceiveWage();
                employee.DisplayEmployeeDetails();
                employee.GiveBonus();
                //employee.AttendManagementMeeting();
            }

            Console.ReadLine();
        }

        static void HasRelationship()
        {
            Employee employeeUno = new Employee("Jake", "Nicols", "jake@snowball.be", new DateTime(1995, 8, 16), 25, "New street", "123", "123456", "Pie Ville");
            string unoAddress = employeeUno.Address.Street;
            Console.WriteLine($"{employeeUno.FirstName} lives on {employeeUno.Address.Street}");

            Address newAddress = new Address("Another street", "444", "999999", "Donut town");
            employeeUno.Address = newAddress;
            Console.WriteLine($"{employeeUno.FirstName} moved to {employeeUno.Address.Street} in {employeeUno.Address.City}");


            Employee mary = new Employee("Mary", "Jones", "mary@snowball.be", new DateTime(1965, 1, 16), 30);
            Employee bobJunior = new Employee("Bob", "Spencer", "bob@snowball.be", new DateTime(1988, 1, 23), 17);
            Employee kevin = new Employee("Kevin", "Marks", "kevin@snowball.be", new DateTime(1953, 12, 12), 10);
            Employee kate = new Employee("Kate", "Greggs", "kate@snowball.be", new DateTime(1993, 8, 8), 10);
            Employee kim = new Employee("Kim", "Jacobs", "kim@snowball.be", new DateTime(1975, 5, 14), 22);

            Employee[] employees = new Employee[7];
            employees[2] = mary;
            employees[3] = bobJunior;
            employees[4] = kevin;
            employees[5] = kate;
            employees[6] = kim;

            foreach (var employee in employees)
            {
                employee.DisplayEmployeeDetails();
                var numberOfHoursWorked = new Random().Next(25);
                employee.PerformWork(numberOfHoursWorked);
                employee.ReceiveWage();
            }

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

        static void IsRelationship()
        {
            Employee employeeOne = new Employee("Bethany", "Smith", "bethany@snowball.be", new DateTime(1979, 1, 16), 25);
            Employee managerOne = new Manager("Mary", "Jones", "mary@snowball.be", new DateTime(1965, 1, 16), 30);
            JuniorResearcher researchOne = new JuniorResearcher("Bob", "Spencer", "bob@snowball.be", new DateTime(1988, 1, 23), 17);
            JuniorResearcher researcherTwo = new JuniorResearcher("Bob", "Spencer", "bob@snowball.be", new DateTime(1988, 1, 23), 17);

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


            //mary.AttendManagementMeeting();
            researchOne.ResearchNewPieTastes(5);
            researcherTwo.ResearchNewPieTastes(5);
            Employee storeManagerOne = new StoreManager("Kevin", "Marks", "kevin@snowball.be", new DateTime(1953, 12, 12), 10);
            Employee storeManagerTwo = new StoreManager("Kate", "Greggs", "kate@snowball.be", new DateTime(1993, 8, 8), 10);
        }
    }
}
