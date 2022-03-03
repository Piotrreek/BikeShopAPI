using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IBikeShopService
    {
        public BikeShopDto GetById(int id);
        public List<BikeShopDto> GetAll();
        public int Create(CreateBikeShopDto dto);
        public void Delete(int id);
        public void Update(int id, UpdateBikeShopDto dto);
    }
}
