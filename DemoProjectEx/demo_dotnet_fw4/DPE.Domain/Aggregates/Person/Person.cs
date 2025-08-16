namespace DPE.Domain.Aggregates.Person
{
    public class Person
    {
        public int Id { get; private set; }
        public Name Name { get; private set; }
        public string PersonType { get; private set; }
        public bool NameStyle { get; private set; }
        public int EmailPromotion { get; private set; }

        public Person(int id, Name name, string personType, bool nameStyle, int emailPromotion)
        {
            Id = id;
            Name = name;
            PersonType = personType;
            NameStyle = nameStyle;
            EmailPromotion = emailPromotion;
        }
    }
}
