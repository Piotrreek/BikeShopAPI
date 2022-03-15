using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IProductService
    {
        public List<ProductDto> GetAll(int shopId);
        public ProductDto Get(int shopId, int id);
        public int Create(int shopId, CreateProductDto dto);
        public void Delete(int shopId, int id);
        public void Update(int shopId, int id, UpdateProductDto dto);
    }
}
