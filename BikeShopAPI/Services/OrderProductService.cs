using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;

namespace BikeShopAPI.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;

        public OrderProductService(BikeShopDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _mapper = mapper;
            _context = context;

        }
        public void BuyNow(int id, BuyNowDto dto)
        {
            var product = _context.Bikes?.FirstOrDefault(b => b.Id == id);
            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }
            if (product.Count <= 0)
            {
                throw new OutOfStockException("This product is out of stock");
            }
            product.Count -= 1;
            var order = _mapper.Map<Order>(dto);
            order.Price = product.Price;
            order.IsPaid = false;
            order.ProductName = product.Name;
            order.UserId = _userContextService.GetUserId;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddToBasket(int id)
        {
            var product = _context.Bikes?.FirstOrDefault(b => b.Id == id);
            if (product is null)
            {
                throw new NotFoundException("Product not found");
            }
            if (product.Count <= 0)
            {
                throw new OutOfStockException("This product is out of stock");
            }
            product.Count -= 1;
            var basket = _context.Baskets.FirstOrDefault(b => b.UserId == _userContextService.GetUserId);
            if (basket is null)
            {
                basket = new Basket()
                {
                    Price = 0,
                    IsPaid = false,
                    UserId = _userContextService.GetUserId,
                    BasketOrders = new List<BasketOrder>()
                };
                _context.Baskets.Add(basket);
                _context.SaveChanges();
            }
            var basketOrder = new BasketOrder()
            {
                Price = product.Price,
                ProductName = product.Name,
                BasketId = basket.Id,
                UserId = _userContextService.GetUserId
            };
            basket.Price += product.Price;
            basket.BasketOrders.Add(basketOrder);
            _context.BasketOrders.Add(basketOrder);
            _context.SaveChanges();
        }
    }
}
