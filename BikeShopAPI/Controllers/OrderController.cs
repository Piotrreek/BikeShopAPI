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
        public ActionResult<List<OrderDto>> GetOrders()
        {
            var orders = _orderService.GetOrders();
            return Ok(orders);
        }
    }
}
