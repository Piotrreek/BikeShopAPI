using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;

namespace BikeShopAPI.Services
{
    public class OrderBikeService : IOrderBikeService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public OrderBikeService(BikeShopDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _mapper = mapper;
            _context = context;
        }
        public void BuyNow(int id, BuyNowDto dto)
        {
            var bike = _context.Bikes?.FirstOrDefault(b => b.Id == id);
            if (bike is null)
            {
                throw new NotFoundException("Bike not found");
            }
            if (bike.Count <= 0)
            {
                throw new OutOfStockException("This bike is out of stock");
            }
            bike.Count -= 1;
            var order = _mapper.Map<Order>(dto);
            order.Price = bike.Price;
            order.IsPaid = false;
            order.ProductName = bike.Name;
            order.UserId = _userContextService.GetUserId;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddToBasket(int id)
        {
            var bike = _context.Bikes?.FirstOrDefault(b => b.Id == id);
            if (bike is null)
            {
                throw new NotFoundException("Bike not found");
            }
            if (bike.Count <= 0)
            {
                throw new OutOfStockException("This bike is out of stock");
            }
            bike.Count -= 1;
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
                Price = bike.Price,
                ProductName = bike.Name,
                BasketId = basket.Id,
                UserId = _userContextService.GetUserId
            };
            basket.Price += bike.Price;
            basket.BasketOrders.Add(basketOrder);
            _context.BasketOrders.Add(basketOrder);
            _context.SaveChanges();
        }
    }
}
