using AdventureWorks.Domain.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;
using static System.Console;

namespace AdventureWorks.ConsoleApp;

internal class Program {
    static void Main(string[] args) {

        WriteLine("Welcome to Adventure works");
        ConnectionGetDepartments();
        ReadLine();
    }

    private static async void ConnectionGetDepartments() {
        //This connection queries the database just fine
        //var connstring = "Data Source=JEANPC-WIN10\\DEVENV;Initial Catalog=AdventureWorks2022;Integrated Security=True;Trust Server Certificate=True";

        var apiUrl = "https://localhost:5001/api/department/connectionstring";
        using var httpClient = new HttpClient();
        var connstring = await httpClient.GetFromJsonAsync<string>(apiUrl);

        var optionsBuilder = new DbContextOptionsBuilder<AdWDbContext>();
        optionsBuilder.UseSqlServer(connstring);
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
