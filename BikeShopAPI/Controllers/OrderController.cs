using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Route("order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public ActionResult<List<OrderDto>> CustomerGetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }
        [HttpGet("all")]
        [Authorize(Roles = "Admin")]
        public ActionResult<List<OrderDto>> AdminGetAllOrders()
        {
            var orders = _orderService.GetAllOrders();
            return Ok(orders);
        }
        [HttpGet("all-manager")]
        [Authorize(Roles = "Manager, Admin")]
        public ActionResult<List<OrderDto>> ManagerGetAllOrders()
        {
            var orders = _orderService.GetAllOrdersByUserId();
            return Ok(orders);
        }
        [HttpGet("basket")]
        public ActionResult<List<OrderDto>> CustomerDisplayBasket()
        {
            var basketOrders = _orderService.DisplayBasket();
            return Ok(basketOrders);
        }
        [HttpPatch("basket")]
        public ActionResult CustomerUpdateBasket([FromBody] BuyNowDto dto)
        {
            _orderService.UpdateBasket(dto);
            return Ok();
        }
        [HttpPatch("basket/{basketId}")]
        public ActionResult ManagerUpdateBasket([FromRoute]int basketId)
        {
            _orderService.UpdateBasketStatus(basketId);
            return Ok();
        }
        [HttpPatch("{orderId}")]
        public ActionResult ManagerUpdateOrder([FromRoute] int orderId)
        {
            _orderService.UpdateOrderStatus(orderId);
            return Ok();
        }
    }
}
