namespace RHJ.InventoryManagement.Domain
{
    public partial class Product
    {
        public static int StockTreshold = 5;
        private void Log(string message) => Console.WriteLine(message);
        
        public static void ChangeStockThreshold(int newStockThreshold) => 
            StockTreshold = newStockThreshold > 0 ? newStockThreshold : StockTreshold;
        private string CreateSimpleProductRepresentation()  => $"Product {Id} ({Name})";        
        // This could be written to a file
           
        

        public void UpdateLowStock()
        {// For now a fixed value  
            if (AmountInStock < StockTreshold)            
                IsBelowStockTreshold = true;
        }
    }
}
