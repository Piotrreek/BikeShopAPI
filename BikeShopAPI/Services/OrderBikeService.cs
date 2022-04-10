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
            order.IsPaid = false;
            order.ProductName = bike.Name;
            order.UserId = _userContextService.GetUserId;
            _context.Orders.Add(order);
            _context.SaveChanges();
        }
    }
}
