using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IOrderBagService
    {
        public void BuyNow(int id, BuyNowDto dto);
        public void AddToBasket(int id);
    }
}
