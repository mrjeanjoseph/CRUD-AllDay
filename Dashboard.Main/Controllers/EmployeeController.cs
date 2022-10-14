using Dashboard.Main.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard.Main.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index() {
            List<Employee> employeeList = new List<Employee>();
            string CS = ConfigurationManager.ConnectionStrings["crudconn"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS)) {
                SqlCommand cmd = new SqlCommand("SELECT * FROM [crud].[Employees]", con);
                cmd.CommandType = CommandType.Text;
                con.Open();

                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read()) {
                    var employee = new Employee();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Age = Convert.ToInt32(rdr["Age"]);
                    employee.Position = rdr["Position"].ToString();
                    employee.Office = rdr["Office"].ToString();
                    employee.HireDate = Convert.ToDateTime(rdr["HireDate"]);
                    employee.Salary = Convert.ToInt32(rdr["Salary"]);
                    employeeList.Add(employee);
                }
            }
            return View(employeeList);
        }
    }
}