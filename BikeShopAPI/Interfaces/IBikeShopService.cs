using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IBikeShopService
    {
        public BikeShopDto GetById(int id);
    }
}
