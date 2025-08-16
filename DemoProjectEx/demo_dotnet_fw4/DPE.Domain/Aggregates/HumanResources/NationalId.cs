using System;

namespace DPE.Domain.Aggregates.HumanResources
{
    public class NationalId
    {
        public string Number { get; }

        public NationalId(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
                throw new ArgumentException("National ID cannot be empty.");

            Number = number;
        }

        public override string ToString() => Number;
    }
}
