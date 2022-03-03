using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Route("shop")]
    [ApiController]
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

        [HttpGet]
        public ActionResult<List<BikeShopDto>> GetAllShops()
        {
            var shops = _bikeShopService.GetAll();
            return Ok(shops);
        }

        [HttpPost]
        public ActionResult CreateBikeShop([FromBody] CreateBikeShopDto dto)
        {
            var id = _bikeShopService.Create(dto);
            return Created($"shop/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteBikeShop([FromRoute] int id)
        {
            _bikeShopService.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateBikeShop([FromRoute] int id, [FromBody] UpdateBikeShopDto dto)
        {
            _bikeShopService.Update(id, dto);
            return Ok();
        }
    }
}
