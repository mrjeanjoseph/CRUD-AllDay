using System.Collections.Generic;

//A Generic method that takes in an item and calls the ToString() method. Then, prints that value to the Console.

namespace GenericsDemo_MT
{
    public class GenericHelper<T> where T : IErrorCheck
    {
        public List<T> QtyCounts { get; set; } = new List<T>();
        public List<T> RejectedItems { get; set; } = new List<T>();

        public void CheckItemToAdd(T item)
        {
            if (item.QtyDiscrepancy == false)
            {
                QtyCounts.Add(item);
            }
            else
            {
                RejectedItems.Add(item);
            }
        }
    }
}

// My work is done!!!