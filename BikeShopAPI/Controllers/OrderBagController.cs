using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("order/bag/{bagId}")]
    [ApiController]
    public class OrderBagController : ControllerBase
    {
        private readonly IOrderBagService _orderService;
        public OrderBagController(IOrderBagService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("buy-now")]
        public ActionResult BuyNow([FromRoute] int bagId, BuyNowDto dto)
        {
            _orderService.BuyNow(bagId, dto);
            return Ok();
        }
        [HttpPost("add-to-basket")]
        public ActionResult AddToBasket([FromRoute] int bagId)
        {
            _orderService.AddToBasket(bagId);
            return Ok();
        }
    }
}
