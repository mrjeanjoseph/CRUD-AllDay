namespace RHJ.InventoryManagement.Domain
{
    public partial class Product
    {
        public static int StockTreshold = 5;

        public static void ChangeStockTreshold(int newStockTreshhold)
        {
            //we will only allow this to go through if the value is > 0
            if (newStockTreshhold > 0)
                StockTreshold = newStockTreshhold;
        }

        //this could be written to a file
        protected void Log(string message) => Console.WriteLine(message);

        protected string CreateSimpleProductRepresentation() => $"Product {Id} ({Name})";        
    }
}
