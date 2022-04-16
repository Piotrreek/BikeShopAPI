using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class OrderBagService : IOrderBagService
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
            var orders = _context.Orders.ToList();
            var oldOrders = orders.Where(o => (DateTime.Now - o.CreatedTime).Days > 14).ToList();
            _context.RemoveRange(oldOrders);
            _context.SaveChanges();
            var bag = _context.Bags?.Include(b => b.Shop).FirstOrDefault(b => b.Id == id);
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
            order.CreatedTime = DateTime.Now;
            order.ShopCreatorId = bag.Shop.CreatedById;
            order.Price = bag.Price;
            order.IsPaid = false;
            order.ProductName = bag.Name;
            order.UserId = _userContextService.GetUserId;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void AddToBasket(int id)
        {
            var orders = _context.BasketOrders.ToList();
            var oldOrders = orders.Where(o => (DateTime.Now - o.CreatedTime).Days > 14).ToList();
            _context.RemoveRange(oldOrders);
            _context.SaveChanges();
            var bag = _context.Bags?.Include(b => b.Shop).FirstOrDefault(b => b.Id == id);
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
                    UserId = _userContextService.GetUserId,
                    BasketOrders = new List<BasketOrder>(),
                    CreatedTime = DateTime.Now,
                    ShopCreatorId = bag.Shop.CreatedById
                };
                _context.Baskets.Add(basket);
                _context.SaveChanges();
            }
            var basketOrder = new BasketOrder()
            {
                Price = bag.Price,
                ProductName = bag.Name,
                BasketId = basket.Id,
                UserId = _userContextService.GetUserId,
                CreatedTime = DateTime.Now,
                ShopCreatorId = bag.Shop.CreatedById
        };
            basket.Price += bag.Price;
            basket.BasketOrders.Add(basketOrder);
            _context.BasketOrders.Add(basketOrder);
            _context.SaveChanges();
        }
    }
}
