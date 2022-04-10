using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("bike/{id}/order")]
    [ApiController]
    public class OrderBikeController : ControllerBase
    {
        private readonly IOrderBikeService _orderService;
        public OrderBikeController(IOrderBikeService orderService)
        {
            _orderService = orderService;
        }
        [Route("buy-now")]
        public ActionResult BuyNow([FromRoute]int id, BuyNowDto dto)
        {
            _orderService.BuyNow(id, dto);
            return Ok();
        }
        public ActionResult AddToBasket()
        {

            return Ok();
        }
    }
}
