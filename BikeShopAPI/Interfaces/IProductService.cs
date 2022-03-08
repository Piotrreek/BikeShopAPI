using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IProductService
    {
        public List<ProductDto> GetAll(int shopId);
    }
}
