Console.WriteLine("Welcome to BossBae's Cuisine");

//Understand implicit conversion
var newMonthlyWage = 1234;
var newHireDAte = new DateTime(2020, 12, 12);
Console.WriteLine(newMonthlyWage.GetType().Name);
Console.WriteLine(newHireDAte.GetType().Name);



Console.ReadLine();
//Understanding Dates
DateTime employeeStart = new DateTime(2024, 04, 07);
Console.WriteLine(employeeStart);

DateTime exitDate = new DateTime(2025, 12, 11);
Console.WriteLine(exitDate);

DateTime startDate = employeeStart.AddDays(15);
Console.WriteLine(startDate);

DateTime currentDate = DateTime.Now;
bool areWeInDST = currentDate.IsDaylightSavingTime();
Console.WriteLine(areWeInDST);

DateTime startHour = DateTime.Now;
TimeSpan workTime = new TimeSpan(8, 35, 0);
DateTime endHour = startHour.Add(workTime);
Console.WriteLine(startHour.ToLongDateString());
Console.WriteLine(endHour.ToShortTimeString());

Console.ReadLine();


//Understanding strings
string aWord = "This is it";
foreach (char c in aWord)
    Console.WriteLine(c);

Console.ReadLine();

int monthlyWage = 12234;

int month = 12, bonus = 1000;

bool isActiv = true;

double rating = 59.15;

byte numberOfEmployee = 125;
//byte numberOfEmployee2 = 625; //Not going to work b/c byte only hold 255

int hoursWorked;
hoursWorked = 125;
hoursWorked = 248;
//monthlyWage = true; //Can't change type

//Understanding const
const double interestRate = 0.07;







Console.WriteLine("Welcome to BossBae's Cuisine");

Console.WriteLine("Please enter your name: ");

string fullName = Console.ReadLine();

Console.WriteLine("Hello " + fullName + "Hope you live the food");