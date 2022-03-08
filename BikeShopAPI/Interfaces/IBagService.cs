using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IBagService
    {
        public List<BagDto> GetAll(int shopId);
        public BagDto Get(int shopId, int bagId);
        public int Create(int shopId, CreateBagDto dto);
        public void Delete(int shopId, int bagId);
        public void Update(int shopId, int bagId, UpdateBagDto dto);
    }
}
