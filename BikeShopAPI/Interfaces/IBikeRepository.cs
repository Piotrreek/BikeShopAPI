using BikeShopAPI.Entities;

namespace BikeShopAPI.Interfaces
{
    public interface IBikeRepository
    {
        public List<Bike>? GetAll();
        public Bike? GetById(int id);
        public List<Bike>? GetByShopId(int shopId);
        public void Insert(Bike bike);
        public void Update(Bike bike);
        public void Delete(Bike bike);
        public void Save();
    }
}
