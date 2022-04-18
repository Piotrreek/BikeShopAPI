using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using FluentEmail.Core;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class OrderProductService : IOrderProductService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IFluentEmail _fluentEmail;
        public OrderProductService(BikeShopDbContext context, IMapper mapper, IUserContextService userContextService, IFluentEmail fluentEmail)
        {
            _userContextService = userContextService;
            _fluentEmail = fluentEmail;
            _mapper = mapper;
            _context = context;
        }
        public void BuyNow(int id, BuyNowDto dto)
        {
            var orders = _context.Orders.ToList();
            var oldOrders = orders.Where(o => (DateTime.Now - o.CreatedTime).Days > 14).ToList();
            _context.RemoveRange(oldOrders);
            _context.SaveChanges();
            var product = _context.Bikes?.Include(b => b.Shop).FirstOrDefault(b => b.Id == id);
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
            order.CreatedTime = DateTime.Now;
            order.ShopCreatorId = product.Shop.CreatedById;
            order.Price = product.Price;
            order.IsPaid = false;
            order.ProductName = product.Name;
            order.UserId = _userContextService.GetUserId;
            var email = _fluentEmail.To("adres@op.pl")
                .Subject("BikeShop order")
                .Body($"Thank you for purchase of {order.ProductName} in our shop. Please transfer {order.Price} PLN to bank account with number 0000000. If we receive money, we will prepare the order and send it to you.");
            email.Send();
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
        public void AddToBasket(int id)
        {
            var basketOrders = _context.BasketOrders.ToList();
            var oldBasketOrders = basketOrders.Where(o => (DateTime.Now - o.CreatedTime).Days > 14).ToList();
            _context.RemoveRange(oldBasketOrders);
            _context.SaveChanges();
            var product = _context.Bikes?.Include(b => b.Shop).FirstOrDefault(b => b.Id == id);
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
                    BasketOrders = new List<BasketOrder>(),
                    CreatedTime = DateTime.Now,
                    ShopCreatorId = product.Shop.CreatedById
                };
                _context.Baskets.Add(basket);
                _context.SaveChanges();
            }
            var basketOrder = new BasketOrder()
            {
                Price = product.Price,
                ProductName = product.Name,
                BasketId = basket.Id,
                UserId = _userContextService.GetUserId,
                CreatedTime = DateTime.Now,
                ShopCreatorId = product.Shop.CreatedById
        };
            basket.Price += product.Price;
            basket.BasketOrders.Add(basketOrder);
            _context.BasketOrders.Add(basketOrder);
            _context.SaveChanges();
        }
    }
}
