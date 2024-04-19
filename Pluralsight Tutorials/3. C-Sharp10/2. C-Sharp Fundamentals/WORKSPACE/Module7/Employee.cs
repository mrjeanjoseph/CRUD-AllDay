using Newtonsoft.Json;
using System.Xml;

namespace ClassesAndObjects {
    public class Employee {
        public string firstName;
        public string lastName;

        public string emailAddress;

        public int numberOfHoursWorked;
        public double standardWage;
        public double wageManager = 1.25;
        public double hourlyRate;
        const int minimumHrsWorkedUnit = 1;

        EmployeeType employeeType;

        public DateTime birthDate;

        //Custom constructors
        //public Employee(string first, string last, string em, DateTime bd) : this(first, last, em, bd, 0) { }
        public Employee(string first, string last, string em, DateTime bd) 
            : this(first, last, em, bd, 0, EmployeeType.StoreManager) { }
        public Employee(string first, string last, string em, DateTime bd, double rate, EmployeeType empType) {
            firstName = first;
            lastName = last;
            emailAddress = em;
            birthDate = bd;
            hourlyRate = rate;
            employeeType = empType;
        }
        public Employee(string first, string last, string em, DateTime bd, double rate) {
            firstName = first;
            lastName = last;
            emailAddress = em;
            birthDate = bd;
            hourlyRate = rate;
        }

        public void PerformWork() {
            //numberOfHoursWorked++;
            PerformWork(minimumHrsWorkedUnit);
            //Console.WriteLine($"{firstName} {lastName} worked for {numberOfHoursWorked} hours.");
        }
        public void PerformWork(int numberOfHours) {
            numberOfHoursWorked += numberOfHours;
            Console.WriteLine($"{firstName} {lastName} worked for {numberOfHours} hours.");
        }

        //Employee can receive wages
        public double ReceiveWage(bool resetHours = true) {
            if (employeeType == EmployeeType.Manager) {
                Console.WriteLine($"Extra wages has been added to {firstName} since he is a store manager");
                standardWage = numberOfHoursWorked * hourlyRate * wageManager;
            }else {
                standardWage = numberOfHoursWorked * hourlyRate;
            }
            Console.WriteLine($"{firstName} {lastName} has recieved a wage of {standardWage} for {numberOfHoursWorked} hours worked.");

            if (resetHours)
                numberOfHoursWorked = 0;            

            return standardWage;
        }

        public void DisplayEmployeeDetails() {
            Console.WriteLine($"\nFirst Name: \t{firstName} \nLast Name: \t{lastName} \nEmail Address: \t{emailAddress} \nBirthDay: \t{birthDate.ToShortDateString()}\n");
        }

        //For Module 8
        public int CalculateBonus(int bonus) {
            if (numberOfHoursWorked > 90)
                bonus *= 5;
            Console.WriteLine($"{firstName} {lastName} has earned a bonus of {bonus} for working more than {numberOfHoursWorked} hours");
            return bonus;
        }

        public int CalculateBonusAndBonusTax(int bonus, ref int bonusTax) {
            if (numberOfHoursWorked > 90) 
                bonus *= 5;

            if (bonus >= 500) {
                bonusTax = bonus / 10;
                bonus -= bonusTax;
            }

            string returnedMsg = $"The tax bonus is {bonusTax}";

            Console.WriteLine($"{firstName} {lastName} has earned a bonus of {bonus} for working more than {numberOfHoursWorked} hours and {returnedMsg.ToLower()}");
            return bonus;

        }

        public int CalculateBonusAndBonusTax2(int bonus, out int bonusTax) {
            bonusTax = 0;

            if (numberOfHoursWorked > 90) 
                bonus *= 5;

            if (bonus >= 500) {
                bonusTax = bonus / 10;
                bonus -= bonusTax;
            }

            string returnedMsg = $"The tax bonus is {bonusTax}";

            Console.WriteLine($"{firstName} {lastName} has earned a bonus of {bonus} for working more than {numberOfHoursWorked} hours and {returnedMsg.ToLower()}");
            return bonus;

        }

        public string ConvertToJson() {
            string myJson = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
            return myJson;
        }
    }
}