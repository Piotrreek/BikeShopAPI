using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BikeShopAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly BikeShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationHandler _authorizationHandler;
        private readonly IUserContextService _userContextService;
        public OrderService(BikeShopDbContext dbContext, IMapper mapper, IAuthorizationHandler authorizationHandler, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationHandler = authorizationHandler;
            _userContextService = userContextService;
        }
        public List<OrderDto> GetOrders()
        {
            var orders = _dbContext.Orders.Where(o => o.UserId == _userContextService.GetUserId).ToList();
            if (orders.Count == 0)
            {
                throw new NullSpecificationException("You do not have any orders");
            }
            return _mapper.Map<List<OrderDto>>(orders);
        }
    }
}
