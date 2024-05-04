using UnderstandingOO.HR;

namespace Module12.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void PerformWorkAddsNumberOfHours()
        {
            //Arrange
            Employee employee = new Employee("Bethany", "Smith", "bethany@snowball.be", new DateTime(1979, 1, 16), 25);

            int numberOfHours = 3;
            //Act
            employee.PerformWork(numberOfHours);

            //Assert
            Assert.Equal(numberOfHours, employee.NumberOfHoursWorked);
        }

        [Fact]
        public void PerformWorkAddsDefaultNumberOfHoursIfNoValueSpecified()
        {
            Employee employee = new Employee("Bethany", "Smith", "bethany@snowball.be", new DateTime(1979, 1, 16), 25);

            employee.PerformWork();

            Assert.Equal(1, employee.NumberOfHoursWorked);
        }
    }
}