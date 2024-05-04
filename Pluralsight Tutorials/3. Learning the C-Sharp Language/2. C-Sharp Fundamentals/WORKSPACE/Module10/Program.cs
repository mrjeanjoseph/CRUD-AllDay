using ClassesAndObjects;

namespace UsingArraysAndLists
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Working with Arrays and Lists");
            Console.WriteLine("=============================");

            ListsOfEmployeeObjects();

            Console.ReadLine();
            WorkingWithCollections();
            WorkingWithArrays();
            ArraysOfEmployeeObjects();
            UnderstandingArrays();
        }

        static void ListsOfEmployeeObjects()
        {

            Console.WriteLine("Let's register some employe Id:");

            int length = int.Parse(Console.ReadLine());

            List<int> employeeIds = new List<int>();
            for (int i = 0; i < length; i++)
            {
                Console.Write("Enter the employee ID: ");
                int id = int.Parse(Console.ReadLine());//let's assume here that the user will always enter an int value
                employeeIds.Add(id);
            }

            Employee employeeOne = new Employee("Tonton", "Bicha", "t-bicha@comedian.ht", new DateTime(1979, 1, 16), 25, EmployeeType.Manager);
            Employee employeeTwo = new("Pe", "Dezirab", "p-dezir@comedian.ht", new DateTime(1984, 3, 28), 30, EmployeeType.Research);
            Employee employeeThree = new Employee("Jean-Claude", "Teraphin", "jc-tera@tecj-ht.com", new DateTime(1965, 1, 16), 30, EmployeeType.Manager);
            Employee employeeFour = new Employee("Baz", "Bob", "bob.radio-caraibe.com", new DateTime(1988, 1, 23), 17, EmployeeType.Research);
            Employee employeeFive = new Employee("Jacques", "Sauveur", "jsauv@sena.ht", new DateTime(1953, 12, 12), 10, EmployeeType.StoreManager);
            Employee employeeSix = new Employee("Francios", "Duvalier", "f-duvalier@haiti.gov", new DateTime(1993, 8, 8), 10, EmployeeType.StoreManager);
            Employee employeeSeven = new Employee("Raoul", "Cedras", "raoul-cedras@haiti.gov", new DateTime(1975, 5, 14), 22, EmployeeType.StoreManager);

            List<Employee> employeeLists = new List<Employee>();
            employeeLists.Add(employeeOne);
            employeeLists.Insert(0, employeeThree);
            employeeLists.Add(employeeSeven);

            employeeLists.Add(employeeSeven);

            employeeLists.Add(employeeSix);
            employeeLists.Add(employeeFive);
            employeeLists.Add(employeeFour);
            //employees.Add(otherEmployee);

            foreach (var employee in employeeLists)
            {
                employee.DisplayEmployeeDetails();
            }

            Console.ReadLine();

        }
        static void WorkingWithCollections()
        {
            List<int> employeeIds = new List<int>();
            employeeIds.Add(515);
            employeeIds.Add(910);
            employeeIds.Add(251);
            employeeIds.Add(358);
            employeeIds.Add(151);

            //employeeIds.Add("Item One"); Don't do that

            if (employeeIds.Contains(251))            
                Console.WriteLine("251 is found!");
            else
                Console.WriteLine("NOT FOUND!");

            int currentAmountOfEmployees = employeeIds.Count;
            var employeeIdsArray = employeeIds.ToArray();
            employeeIds.Clear();
        }
        static void WorkingWithArrays()
        {
            Console.WriteLine("Let's use common methods arrays");

            int length = int.Parse(Console.ReadLine());

            int[] employeeIds = new int[length];

            for (int i = 0; i < length; i++)
            {
                Console.Write("Enter the employee ID: ");
                int id = int.Parse(Console.ReadLine());
                employeeIds[i] = id;
            }

            //Not sorted
            Console.WriteLine("Original Arrays:");
            for (int i = 0; i < employeeIds.Length; i++)
            {
                Console.WriteLine($"ID {i + 1}: \t{employeeIds[i]}");
            }

            Array.Sort(employeeIds);
            //Sorted
            Console.WriteLine("Sorted Arrays");
            for (int i = 0; i < employeeIds.Length; i++)
            {
                Console.WriteLine($"ID {i + 1}: \t{employeeIds[i]}");
            }

            int[] employeeIdsCopy = new int[length];

            employeeIds.CopyTo(employeeIdsCopy, 0);

            Array.Reverse(employeeIds);

            Console.WriteLine("Reversed Array");

            for (int i = 0; i < employeeIds.Length; i++)
            {
                Console.WriteLine($"ID {i + 1}: \t{employeeIds[i]}");
            }

            Console.WriteLine("Copy of array");

            for (int i = 0; i < employeeIdsCopy.Length; i++)
            {
                Console.WriteLine($"ID {i + 1}: \t{employeeIdsCopy[i]}");
            }
        }
        static void ArraysOfEmployeeObjects()
        {
            Console.WriteLine("Arrays of Employee Objects");

            Employee employeeOne = new Employee("Tonton", "Bicha", "t-bicha@comedian.ht", new DateTime(1979, 1, 16), 25, EmployeeType.Manager);
            Employee employeeTwo = new("Pe", "Dezirab", "p-dezir@comedian.ht", new DateTime(1984, 3, 28), 30, EmployeeType.Research);
            Employee employeeThree = new Employee("Jean-Claude", "Teraphin", "jc-tera@tecj-ht.com", new DateTime(1965, 1, 16), 30, EmployeeType.Manager);
            Employee employeeFour = new Employee("Baz", "Bob", "bob.radio-caraibe.com", new DateTime(1988, 1, 23), 17, EmployeeType.Research);
            Employee employeeFive = new Employee("Jacques", "Sauveur", "jsauv@sena.ht", new DateTime(1953, 12, 12), 10, EmployeeType.StoreManager);
            Employee employeeSix = new Employee("Francios", "Duvalier", "f-duvalier@haiti.gov", new DateTime(1993, 8, 8), 10, EmployeeType.StoreManager);
            Employee employeeSeven = new Employee("Raoul", "Cedras", "raoul-cedras@haiti.gov", new DateTime(1975, 5, 14), 22, EmployeeType.StoreManager);

            Employee[] employees = new Employee[7];

            employees[0] = employeeOne;
            employees[1] = employeeTwo;
            employees[2] = employeeThree;
            employees[3] = employeeFour;
            employees[4] = employeeFive;
            employees[5] = employeeSix;
            employees[6] = employeeSeven;

            foreach (var emp in employees)
            {
                emp.DisplayEmployeeDetails();
                var numberOfHoursWorked = new Random().Next(25);
                emp.PerformWork(numberOfHoursWorked);
                emp.ReceiveWage();
            }
        }
        static void UnderstandingArrays()
        {

            Console.WriteLine("How many employees IDs do you want to register?");

            int length = int.Parse(Console.ReadLine());

            int[] employeeIds = new int[length];

            //var testId = employeeIds[0];
            //Console.WriteLine(testId);

            for (int i = 0; i < length; i++)
            {
                Console.Write("Enter the employee ID: ");
                int id = int.Parse(Console.ReadLine());//let's assume here that the user will always enter an int value
                employeeIds[i] = id;
            }

            Console.WriteLine("Here are the Id:");
            for (int i = 0; i < employeeIds.Length; i++)
            {
                Console.WriteLine($"ID {i + 1}: \t{employeeIds[i]}");
            }
        }
    }
}
