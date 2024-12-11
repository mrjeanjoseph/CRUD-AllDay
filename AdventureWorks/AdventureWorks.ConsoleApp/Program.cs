using AdventureWorks.Domain.DataAccessLayer;
using AdventureWorks.DomainRepository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.Design;
using static System.Console;


namespace AdventureWorks.ConsoleApp {
    internal class Program {
        static void Main(string[] args) {

            WriteLine("Welcome to Adventure works");
            ConnectionGetDepartments();
            ReadLine();
        }

        private static void ConnectionGetDepartments() {
            //This connection queries the database just fine

            var optionsBuilder = new DbContextOptionsBuilder<AdWDbContext>();
            optionsBuilder.UseSqlServer("Data Source=JEANPC\\DEVSERVER;Initial Catalog=AdventureWorks2022;Integrated Security=True;Encrypt=True");

            using (var context = new AdWDbContext(optionsBuilder.Options)) {
                var departments = context.Departments.ToList();

                foreach (var dept in departments) {
                    WriteLine($"Id: {dept.DepartmentId}, Name: {dept.Name}, Price: {dept.GroupName}");
                }
            }
        }
    }
}
