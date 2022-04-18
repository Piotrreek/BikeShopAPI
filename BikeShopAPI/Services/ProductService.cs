using AutoMapper;
using BikeShopAPI.Authorization;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public ProductService(BikeShopDbContext context, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _context = context;
        }
        public List<ProductDto> GetAll(int shopId)
        {
            var isShop = CheckIfShopExists(shopId);
            if (isShop == false)
            {
                throw new NotFoundException("Shop not found");
            }
            var shop = _context.BikeShops
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == shopId);
            var products = shop.Products;
            var productsDto = _mapper.Map<List<ProductDto>>(products);
            if (productsDto is null)
            {
                throw new NullSpecificationException("This shop does not have any products");
            }
            return productsDto;
        }
        public ProductDto Get(int shopId, int id)
        {
            var isShop = CheckIfShopExists(shopId);
            if (isShop == false)
            {
                throw new NotFoundException("Shop not found");
            }
            var shop = _context.BikeShops
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == shopId);
            var product = shop.Products?
                .FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                throw new NotFoundException("Product not found");
            }
            var productDto = _mapper.Map<ProductDto>(product);
            return productDto;
        }
        public int Create(int shopId, CreateProductDto dto)
        {
            var isShop = CheckIfShopExists(shopId);
            if (isShop == false)
            {
                throw new NotFoundException("Shop not found");
            }
            var shop = _context.BikeShops
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == shopId);
            var product = _mapper.Map<Product>(dto);
            product.BikeShopId = shopId;
            product.CreatedById = _userContextService.GetUserId;
            _context.Add(product);
            _context.SaveChanges();
            return product.Id;
        }
        public void Delete(int shopId, int id)
        {
            var isShop = CheckIfShopExists(shopId);
            if (isShop == false)
            {
                throw new NotFoundException("Shop not found");
            }
            var shop = _context.BikeShops
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == shopId);
            var productExists = CheckIfProductExists(id);
            if (productExists == false)
            {
                throw new NotFoundException("Product not found");
            }
            var product = shop.Products?.FirstOrDefault(p => p.Id == id && p.BikeShopId == shopId);
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, product, new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            _context.Remove(product);
            _context.SaveChanges();
        }
        public void Update(int shopId, int id, UpdateProductDto dto)
        {
            var isShop = CheckIfShopExists(shopId);
            if (isShop == false)
            {
                throw new NotFoundException("Shop not found");
            }
            var shop = _context.BikeShops
                .Include(s => s.Products)
                .FirstOrDefault(s => s.Id == shopId);
            CheckIfProductExists(id);
            var product = shop.Products?.FirstOrDefault(s => s.Id == id && s.BikeShopId == shopId);
            if (product is null)
            {
                throw new NullSpecificationException("This shop does not have such product");
            }
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, product, new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            product = _mapper.Map(dto, product);
            _context.SaveChanges();
        }
        private bool CheckIfProductExists(int productId)
        {
            var product = _context?.Products?
                .FirstOrDefault(p => p.Id == productId);
            return product != null;
        }
        private bool CheckIfShopExists(int shopId)
        {
            var shop = _context.BikeShops
                .FirstOrDefault(s => s.Id == shopId);
            return shop != null;
        }
    }
}
