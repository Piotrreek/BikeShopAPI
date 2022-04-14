using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("product/{id}/order")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderService;
        public OrderProductController(IOrderProductService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("buy-now")]
        public ActionResult BuyNow([FromRoute] int id, BuyNowDto dto)
        {
            _orderService.BuyNow(id, dto);
            return Ok();
        }
        [HttpPost("add-to-basket")]
        public ActionResult AddToBasket([FromRoute] int id)
        {
            _orderService.AddToBasket(id);
            return Ok();
        }
    }
}
