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
    public class OrderService : IOrderService
    {
        private readonly BikeShopDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public OrderService(BikeShopDbContext dbContext, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authorizationService = authorizationService;
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
        public List<OrderDto> GetAllOrders()
        {
            var orders = _dbContext.Orders.ToList();
            if (orders.Count == 0)
            {
                throw new NullSpecificationException("You do not have any orders");
            }
            return _mapper.Map<List<OrderDto>>(orders);
        }
        public List<OrderDto> GetAllOrdersByUserId()
        {
            var orders = _dbContext.Orders.Where(o => o.ShopCreatorId == _userContextService.GetUserId).ToList();
            if (orders.Count == 0)
            {
                throw new NullSpecificationException("There aren't any orders at your shop right now");
            }
            return _mapper.Map<List<OrderDto>>(orders);
        }
        public List<OrderDto> DisplayBasket()
        {
            var basketOrders = _dbContext.Baskets
                .Include(b => b.BasketOrders)
                .FirstOrDefault(b => b.UserId == _userContextService.GetUserId)?
                .BasketOrders
                .ToList();
            if (basketOrders == null || basketOrders.Count == 0)
            {
                throw new NullSpecificationException("You do not have any orders in your basket");
            }
            return _mapper.Map<List<OrderDto>>(basketOrders);
        }
        public void UpdateBasket(BuyNowDto dto)
        {
            var basket = _dbContext.Baskets.FirstOrDefault(b => b.UserId == _userContextService.GetUserId);
            if (basket is null)
            {
                throw new NullSpecificationException("Your basket is empty");
            }
            _mapper.Map(dto, basket);
            _dbContext.SaveChanges();
        }
        public void UpdateBasketStatus(int basketId)
        {
            var basket = _dbContext.Baskets.FirstOrDefault(b => b.Id == basketId);
            if (basket is null)
            {
                throw new NullSpecificationException("Your basket is empty");
            }
            var authResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, basket, new OperationRequirement(Operation.Update)).Result;
            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }
            basket.IsPaid = !basket.IsPaid;
            _dbContext.SaveChanges();
        }
        public void UpdateOrderStatus(int orderId)
        {
            var order = _dbContext.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order is null)
            {
                throw new NotFoundException("Order not found");
            }
            var authResult = _authorizationService.AuthorizeAsync(_userContextService.User, order,
                new OperationRequirement(Operation.Update)).Result;
            if (!authResult.Succeeded)
            {
                throw new ForbidException();
            }
            order.IsPaid = !order.IsPaid;
            _dbContext.SaveChanges();
        }
    }
}
