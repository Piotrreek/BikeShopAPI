using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface ISpecificationService
    {
        public List<SpecificationDto> GetSpecOfBike(int bikeId);
        public void Create(int bikeId, CreateSpecificationDto dto);
        public void Delete(int bikeId);
        public void DeleteById(int bikeId, int specId);
        public void Update(int bikeId, int specId, UpdateSpecificationDto dto);
    }
}
