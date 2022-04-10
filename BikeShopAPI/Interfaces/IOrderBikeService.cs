using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IOrderBikeService
    {
        public void BuyNow(int id, BuyNowDto dto);
    }
}
