// PROPTIP: The standard generic variable is T but you can choose any letter you want.
// HOMEWORK: Create a generic method that takes in an item and calls the ToString() method an prints that value to the Console.

namespace GenericsDemo_MT
{
    public class InventoryModel : IErrorCheck
    {
        public string InventoryItem { get; set; }
        public int InventoryCount { get; set; }
        public bool QtyDiscrepancy { get; set; }
    }
}
