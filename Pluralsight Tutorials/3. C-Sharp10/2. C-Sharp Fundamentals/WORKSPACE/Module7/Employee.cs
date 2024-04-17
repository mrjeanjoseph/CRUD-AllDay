namespace ClassesAndObjects {
    internal class Employee {
        public string firstName;
        public string lastName;

        public string emailAddress;

        public int numberOfHoursWorked;
        public double standardWage;
        public double hourlyRate;
        const int minimumHrsWorkedUnit = 1;

        public DateTime birthDate;

        //Custom constructors
        public Employee(string first, string last, string em, DateTime bd) 
            : this(first, last, em, bd, 0) { }
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
            standardWage = numberOfHoursWorked * hourlyRate;
            Console.WriteLine($"{firstName} {lastName} has recieved a wage of {standardWage} for {numberOfHoursWorked} hours worked.");

            if (resetHours) {
                numberOfHoursWorked = 0;
            }

            return standardWage;
        }

        public void DisplayEmployeeDetails() {
            Console.WriteLine($"\nFirst Name: \t{firstName} \nLast Name: \t{lastName} \nEmail Address: \t{emailAddress} \nBirthDay: \t{birthDate.ToShortDateString()}\n");
        }
    }
}