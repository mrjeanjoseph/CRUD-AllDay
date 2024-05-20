namespace RHJ.InventoryManagement.Domain
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int AmountOrdered { get; set; }

        public override string ToString() => $"Product Id: {ProductId} - Product Name: {ProductName} - Amount Ordered: {AmountOrdered}";
    }
}
