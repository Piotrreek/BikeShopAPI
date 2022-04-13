using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("bag/{id}/order")]
    [ApiController]
    public class OrderBagController : ControllerBase
    {
        private readonly IOrderBagService _orderService;
        public OrderBagController(IOrderBagService orderService)
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
