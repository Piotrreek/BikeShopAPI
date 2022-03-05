using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IBikeService
    {
        public List<BikeDto> GetAll(int bikeShopId);
        public BikeDto Get(int id);
        public int Create(int bikeShopId, CreateBikeDto dto);
        public List<BikeDto> GetAllWithoutId();
        public void Delete(int id);
        public void Update(int id, UpdateBikeDto dto);
    }
}
