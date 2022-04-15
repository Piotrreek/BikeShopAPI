using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IOrderService
    {
        public List<OrderDto> GetOrders();
        public List<OrderDto> GetAllOrders();
        public List<OrderDto> GetAllOrdersByUserId();
        public List<OrderDto> DisplayBasket();
        public void UpdateBasket(BuyNowDto dto);
    }
}
