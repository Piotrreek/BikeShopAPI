using BikeShopAPI.Entities;

namespace BikeShopAPI.Interfaces
{
    public interface IBikeShopRepository
    {
        IEnumerable<BikeShop> GetAll();
        BikeShop? GetById(int id);
        void Insert(BikeShop obj);
        void Update(BikeShop obj);
        void Delete(BikeShop obj);
    }
}
