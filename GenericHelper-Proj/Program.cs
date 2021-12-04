using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// PROPTIP: The standard generic variable is T but you can choose any letter you want.
// HOMEWORK: Create a generic method that takes in an item and calls the ToString() method an prints that value to the Console.

namespace GenericsDemo_MT
{
    class Program
    {
        static void Main(string[] args)
        {
            CallTheToString();
            Console.ReadLine();
        }

        public static void CallTheToString()
        {
            GenericHelper<InventoryModel> InventoryQtyCounts = new GenericHelper<InventoryModel>();
            InventoryQtyCounts.CheckItemToAdd(new InventoryModel { InventoryItem = "Toyota Tundra", QtyDiscrepancy = true });

            foreach (var item in InventoryQtyCounts.RejectedItems)
            {
                Console.WriteLine($" { item.InventoryItem } was rejected");
            }
        }
    }
}
