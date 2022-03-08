using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(BikeShopDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public List<ProductDto> GetAll(int shopId)
        {
            var shop = CheckIfShopExists(shopId);
            var products = shop.Products;
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            if (productsDto is null)
            {
                throw new NullSpecificationException("This shop does not have any products");
            }
            return productsDto;
        }
        private BikeShop CheckIfShopExists(int shopId)
        {
            var shop = _context.BikeShops
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == shopId);
            if (shop == null)
            {
                throw new NotFoundException("Shop not found");
            }
            return shop;
        }
    }
}
