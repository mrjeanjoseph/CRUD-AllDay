namespace RHJ.InventoryManagement.Domain
{
    public class Order
    {
        public int Id { get; private set; }
        public DateTime OrderFulfilementDate { get; private set; }
        public List<OrderItem> OrderItems { get; }
        public bool Fulfilled { get; set; } = false;

        public Order()
        {
            Id = new Random().Next(9999999);

            int numOfSecs = new Random().Next(100);
            OrderFulfilementDate = DateTime.Now.AddSeconds(numOfSecs);

            OrderItems = new List<OrderItem>();
        }

    }

}
