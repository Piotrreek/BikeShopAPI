using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("order/product/{productId}")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderService;
        public OrderProductController(IOrderProductService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("buy-now")]
        public ActionResult BuyNow([FromRoute] int productId, BuyNowDto dto)
        {
            _orderService.BuyNow(productId, dto);
            return Ok();
        }
        [HttpPost("add-to-basket")]
        public ActionResult AddToBasket([FromRoute] int productId)
        {
            _orderService.AddToBasket(productId);
            return Ok();
        }
    }
}
