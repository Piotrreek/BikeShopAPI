using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("order/bike/{bikeId}")]
    [ApiController]
    public class OrderBikeController : ControllerBase
    {
        private readonly IOrderBikeService _orderService;
        public OrderBikeController(IOrderBikeService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("buy-now")]
        public ActionResult BuyNow([FromRoute]int bikeId, BuyNowDto dto)
        {
            _orderService.BuyNow(bikeId, dto);
            return Ok();
        }
        [HttpPost("add-to-basket")]
        public ActionResult AddToBasket([FromRoute]int bikeId)
        {
            _orderService.AddToBasket(bikeId);
            return Ok();
        }
    }
}
