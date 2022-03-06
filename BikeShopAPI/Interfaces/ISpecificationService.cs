using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface ISpecificationService
    {
        public List<SpecificationDto> GetSpecOfBike(int bikeId);
        public void Create(int bikeId, CreateSpecificationDto dto);
    }
}
