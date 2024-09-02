using SportsStore.Domain.Entities;

namespace SportsStore.Domain.Abstract {
    internal interface IOrderProcessor {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
