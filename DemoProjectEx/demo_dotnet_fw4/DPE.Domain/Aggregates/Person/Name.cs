namespace DPE.Domain.Aggregates.Person
{
    public class Name
    {
        public string Title { get; }
        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }
        public string Suffix { get; }

        public Name(string title, string firstName, string middleName, string lastName, string suffix)
        {
            Title = title;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Suffix = suffix;
        }

        public string GetFullName() =>
            $"{Title} {FirstName} {MiddleName} {LastName} {Suffix}".Replace("  ", " ").Trim();
    }
}
