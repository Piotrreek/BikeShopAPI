using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IOrderService
    {
        public List<OrderDto> GetOrders();
    }
}
