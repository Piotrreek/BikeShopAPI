using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Route("shop")]
    //[ApiController]
    public class BikeShopController : ControllerBase
    {
        private readonly IBikeShopService _bikeShopService;
        public BikeShopController(IBikeShopService bikeShopService)
        {
            _bikeShopService = bikeShopService;
        }

        [HttpGet("{id}")]
        public ActionResult<BikeShopDto> GetShopById([FromRoute]int id)
        {
            var shop = _bikeShopService.GetById(id);
            return Ok(shop);
        }


    }
}
