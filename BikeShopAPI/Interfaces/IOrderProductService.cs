using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IOrderProductService
    {
        public void BuyNow(int id, BuyNowDto dto);
        public void AddToBasket(int id);
    }
}
