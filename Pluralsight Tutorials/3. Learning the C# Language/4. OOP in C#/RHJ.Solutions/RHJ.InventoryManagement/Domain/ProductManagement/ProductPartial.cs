namespace RHJ.InventoryManagement.Domain
{
    public partial class Product
    {
        private void Log(string message) => Console.WriteLine(message);
        
        private string CreateSimpleProductRepresentation()  => $"Product {Id} ({Name})";        
        // This could be written to a file
           
        

        private void UpdateLowStock()
        {// For now a fixed value  
            if (AmountInStock < 10)            
                IsBelowStockThreshold = true;
        }
    }
}
