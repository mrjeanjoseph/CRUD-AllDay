using AdventureWorks.Domain.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using static System.Console;

namespace AdventureWorks.ConsoleApp;

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
        //The plan is move this to the appsettings.json file.

        using var context = new AdWDbContext(optionsBuilder.Options);
        var departments = context.Departments.ToList();
        var counts = 0;
        foreach (var dept in departments) {
            counts++;
            WriteLine($"-{counts} -- Id: {dept.DepartmentId}, Name: {dept.Name}, Price: {dept.GroupName}");

        }
    }
}
