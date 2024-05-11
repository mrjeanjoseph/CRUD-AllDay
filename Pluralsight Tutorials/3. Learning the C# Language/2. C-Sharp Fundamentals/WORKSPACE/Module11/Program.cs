using UnderstandingOO.HR;

namespace UnderstandingOO
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            UsingIterfaces();

            Console.ReadLine();
            HasRelationship();
            IsRelationship();
            BaseAndDerived();
            AddEncapsulation();
        }

        static void UsingIterfaces()
        {

            IEmployee storeMan = new StoreManager("Elvila", "Jean-Joseph", "Elvila@snowball.be", new DateTime(1979, 1, 16), 25);
            IEmployee regManager = new Manager("Julie", "Dure", "Julie@snowball.be", new DateTime(1965, 1, 16), 30);
            JuniorResearcher jrResearcher = new JuniorResearcher("Bazbob", "Larivierre", "Bazbob@snowball.be", new DateTime(1988, 1, 23), 17);
            IEmployee storeMan2 = new StoreManager("Robin", "Jean-Jacques", "Robin@snowball.be", new DateTime(1953, 12, 12), 10);
            IEmployee storeMan3 = new StoreManager("Manoushca", "Saint-Joie", "Manoushca@snowball.be", new DateTime(1993, 8, 8), 10);

            storeMan.DisplayEmployeeDetails();
            storeMan.PerformWork();
            storeMan.PerformWork(10);
            storeMan.PerformWork();
            storeMan.ReceiveWage();

            regManager.DisplayEmployeeDetails();
            regManager.PerformWork();
            regManager.PerformWork(3);
            regManager.PerformWork();
            //Julie.AttendManagementMeeting();
            regManager.ReceiveWage();

            jrResearcher.ResearchNewPieTastes(5);
            jrResearcher.ReceiveWage();

            List<IEmployee> employees = [ storeMan, regManager, jrResearcher, storeMan2, storeMan3, ];

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


            Employee Julie = new Employee("Julie", "Dure", "Julie@snowball.be", new DateTime(1965, 1, 16), 30);
            Employee BazbobJunior = new Employee("Bazbob", "Larivierre", "Bazbob@snowball.be", new DateTime(1988, 1, 23), 17);
            Employee Robin = new Employee("Robin", "Jean-Jacques", "Robin@snowball.be", new DateTime(1953, 12, 12), 10);
            Employee Manoushca = new Employee("Manoushca", "Saint-Joie", "Manoushca@snowball.be", new DateTime(1993, 8, 8), 10);
            Employee kim = new Employee("Kim", "Jacobs", "kim@snowball.be", new DateTime(1975, 5, 14), 22);

            Employee[] employees = new Employee[7];
            employees[2] = Julie;
            employees[3] = BazbobJunior;
            employees[4] = Robin;
            employees[5] = Manoushca;
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
            Employee employeeOne = new Employee("Elvila", "Jean-Joseph", "Elvila@snowball.be", new DateTime(1979, 1, 16), 25);
            Manager managerOne = new Manager("Julie", "Dure", "Julie@snowball.be", new DateTime(1965, 1, 16), 30);
            JuniorResearcher BazbobJunior = new JuniorResearcher("Bazbob", "Larivierre", "Bazbob@snowball.be", new DateTime(1988, 1, 23), 17);

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
            BazbobJunior.ResearchNewPieTastes(5);
            BazbobJunior.ResearchNewPieTastes(5);
        }

        static void AddEncapsulation()
        {
            Employee employeeOne = new Employee("Elvila", "Jean-Joseph", "Elvila@snowball.be", new DateTime(1979, 1, 16), 25);
            Employee employeeTwo = new("George", "Dure", "george@snowball.be", new DateTime(1984, 3, 28), 30);

            employeeOne.DisplayEmployeeDetails();
            employeeOne.PerformWork(8);
            employeeOne.PerformWork();
            employeeOne.PerformWork(3);
            employeeOne.ReceiveWage();

            //Elvila.firstName = "John";
            employeeOne.FirstName = "John";
            employeeOne.DisplayEmployeeDetails();

            //Elvila.Wage = 300;
        }

        static void IsRelationship()
        {
            Employee employeeOne = new Employee("Elvila", "Jean-Joseph", "Elvila@snowball.be", new DateTime(1979, 1, 16), 25);
            Employee managerOne = new Manager("Julie", "Dure", "Julie@snowball.be", new DateTime(1965, 1, 16), 30);
            JuniorResearcher researchOne = new JuniorResearcher("Bazbob", "Larivierre", "Bazbob@snowball.be", new DateTime(1988, 1, 23), 17);
            JuniorResearcher researcherTwo = new JuniorResearcher("Bazbob", "Larivierre", "Bazbob@snowball.be", new DateTime(1988, 1, 23), 17);

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


            //Julie.AttendManagementMeeting();
            researchOne.ResearchNewPieTastes(5);
            researcherTwo.ResearchNewPieTastes(5);
            Employee storeManagerOne = new StoreManager("Robin", "Jean-Jacques", "Robin@snowball.be", new DateTime(1953, 12, 12), 10);
            Employee storeManagerTwo = new StoreManager("Manoushca", "Saint-Joie", "Manoushca@snowball.be", new DateTime(1993, 8, 8), 10);
        }
    }
}
