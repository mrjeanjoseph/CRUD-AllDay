namespace SportsStore.Domain
{
    public interface IOrderProceessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
