using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;

namespace BikeShopAPI.Services
{
    public class OrderBagService : IOrderService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        public OrderBagService(BikeShopDbContext context, IMapper mapper, IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _mapper = mapper;
            _context = context;
        }
        public void BuyNow(int id, BuyNowDto dto)
        {
            var bag = _context.Bags?.FirstOrDefault(b => b.Id == id);
            if (bag is null)
            {
                throw new NotFoundException("Bag not found");
            }
            if (bag.Count <= 0)
            {
                throw new OutOfStockException("This bag is out of stock");
            }
            bag.Count -= 1;
            var order = _mapper.Map<Order>(dto);
            order.Price = bag.Price;
            order.IsPaid = false;
            order.ProductName = bag.Name;
            order.UserId = _userContextService.GetUserId;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddToBasket(int id)
        {
            var bag = _context.Bags?.FirstOrDefault(b => b.Id == id);
            if (bag is null)
            {
                throw new NotFoundException("Bike not found");
            }
            if (bag.Count <= 0)
            {
                throw new OutOfStockException("This bike is out of stock");
            }
            bag.Count -= 1;
            var basket = _context.Baskets.FirstOrDefault(b => b.UserId == _userContextService.GetUserId);
            if (basket is null)
            {
                basket = new Basket()
                {
                    Price = 0,
                    IsPaid = false,
                    UserId = _userContextService.GetUserId
                };
                _context.Baskets.Add(basket);
                _context.SaveChanges();
            }
            var basketOrder = new BasketOrder()
            {
                Price = bag.Price,
                ProductName = bag.Name,
                BasketId = basket.Id,
                UserId = _userContextService.GetUserId
            };
            basket.Price += bag.Price;
            _context.BasketOrders.Add(basketOrder);
            _context.SaveChanges();
        }
    }
}
