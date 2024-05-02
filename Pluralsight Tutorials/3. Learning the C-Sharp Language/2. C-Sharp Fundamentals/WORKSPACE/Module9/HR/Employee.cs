using BethanysPieShopHRM.Logic;

namespace ClassesAndCustomTypes.HR
{
    internal class Employee
    {

        public string fullName;
        public string email;

        public int numberOfHoursWorked;
        public double wage;
        public double? hourlyRate;
        public EmployeeType employeeType;

        public DateTime birthDay;

        const int minimalHoursWorkedUnit = 1;

        public static double taxRate = 0.15; //We did static already sir

        public Employee(string fn, string em, DateTime bd, double? rate, EmployeeType empType)
        {
            fullName = fn;
            email = em;
            birthDay = bd;
            hourlyRate = rate ?? 10;
            employeeType = empType;
        }

        public Employee(string fn, string em, DateTime bd)
            : this(fn, em, bd, 0, EmployeeType.StoreManager) { }

        public void PerformWork()
        {
            PerformWork(minimalHoursWorkedUnit);
        }

        public void PerformWork(int numberOfHours)
        {
            numberOfHoursWorked += numberOfHours;

            Console.WriteLine($"{fullName} has worked for {numberOfHours} hour(s)!");
        }

        public int CalculateBonus(int bonus)
        {

            if (numberOfHoursWorked > 10)
                bonus *= 2;

            Console.WriteLine($"The employee got a bonus of {bonus}");
            return bonus;
        }

        public int CalculateBonusAndBonusTax(int bonus, out int bonusTax)
        {
            bonusTax = 0;
            if (numberOfHoursWorked > 10)
                bonus *= 2;

            if (bonus >= 200)
            {
                bonusTax = bonus / 10;
                bonus -= bonusTax;
            }

            Console.WriteLine($"The employee got a bonus of {bonus} and the tax on the bonus is {bonusTax}");
            return bonus;
        }

        public double CalculateWage()
        {
            WageCalculations wagecalc = new WageCalculations();
            double calcValue = wagecalc.ComplexWageCalculation(wage, taxRate, 3, 42);
            return calcValue;
        }


        public double ReceiveWage(bool resetHours = true)
        {
            double wageBeforeTax;

            if (employeeType == EmployeeType.Manager)
            {
                Console.WriteLine($"An extra ${hourlyRate * 1.25} was added to the wage since {fullName} is a manager!");
                wageBeforeTax = numberOfHoursWorked * hourlyRate.Value * 1.25;
            }
            else
            {
                wageBeforeTax = numberOfHoursWorked * hourlyRate.Value;
            }

            double taxAmount = wageBeforeTax * taxRate;

            wage = wageBeforeTax - taxAmount;

            Console.WriteLine($"{fullName} has received a wage of {wage} for {numberOfHoursWorked} hour(s) of work.");

            if (resetHours)
                numberOfHoursWorked = 0;

            return wage;
        }

        public void DisplayEmployeeDetails()
        {
            string firstName = fullName.Split(' ')[0];
            string lastName = fullName.Split(' ')[1];
            Console.WriteLine($"\nFirst Name: \t{firstName}\nLast Name: \t{lastName}\nEmail: \t\t{email}\nBirthday: \t{birthDay.ToShortDateString()}\nTax rate: \t{taxRate}");
        }
    }
}
