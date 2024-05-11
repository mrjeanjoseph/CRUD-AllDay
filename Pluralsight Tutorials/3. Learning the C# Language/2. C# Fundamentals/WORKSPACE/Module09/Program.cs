using ClassesAndCustomTypes.HR;

namespace ClassesAndCustomTypes {
    internal class Program {
        static void Main(string[] args) {
            Console.WriteLine("Creating a new employee");
            Console.WriteLine("---------------------\n");

            //Working with null
            Employee nullEmp = null;
            nullEmp.DisplayEmployeeDetails();

            Employee employeeOne = new("Nitaud Paniague", "npaniague@dvc.com", new DateTime(1979, 1, 16), 25, EmployeeType.Manager);

            Employee employeeTwo = new("Denzel Paniague", "dpaniague@dvc.com", new DateTime(1984, 3, 28), 30, EmployeeType.Researcher);

            #region First run employeeOne
            employeeTwo.PerformWork();
            employeeOne.PerformWork(5);
            employeeOne.PerformWork();
            employeeOne.ReceiveWage();
            employeeOne.DisplayEmployeeDetails();
            #endregion

            #region Then run employeeTwo
            employeeTwo.PerformWork(10);
            employeeTwo.PerformWork();
            employeeTwo.PerformWork();
            employeeTwo.ReceiveWage();
            employeeTwo.DisplayEmployeeDetails();
            #endregion

            employeeTwo.CalculateWage();
            Console.ReadLine();
        }

        public static void ExplainingCarbageCollection() {

            List<Employee> employees = new List<Employee>();
            for (int i = 0; i < 5000; i++) {
                Employee randomEmployee = new(Guid.NewGuid().ToString(),
                    Guid.NewGuid().ToString(), new DateTime(1985, 10, 7),
                    null, EmployeeType.Vendor);
                employees.Add(randomEmployee);
            }

            employees.Clear();
            employees = null;

            GC.Collect(); //we can force Garbage Collection to run
        }
    }
}
