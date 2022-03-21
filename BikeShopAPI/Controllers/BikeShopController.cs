using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("shop")]
    [ApiController]
    public class BikeShopController : ControllerBase
    {
        private readonly IBikeShopService _bikeShopService;
        public BikeShopController(IBikeShopService bikeShopService)
        {
            _bikeShopService = bikeShopService;
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<BikeShopDto> GetShopById([FromRoute]int id)
        {
            var shop = _bikeShopService.GetById(id);
            return Ok(shop);
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<BikeShopDto>> GetAllShops()
        {
            var shops = _bikeShopService.GetAll();
            return Ok(shops);
        }
        [Authorize(Roles ="Admin,Manager")]
        [HttpPost]
        public ActionResult CreateBikeShop([FromBody] CreateBikeShopDto dto)
        {
            var id = _bikeShopService.Create(dto);
            return Created($"shop/{id}", null);
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public ActionResult DeleteBikeShop([FromRoute] int id)
        {
            _bikeShopService.Delete(id);
            return NoContent();
        }
        [Authorize(Roles = "Admin,Manager")]
        [HttpPatch("{id}")]
        public ActionResult UpdateBikeShop([FromRoute] int id, [FromBody] UpdateBikeShopDto dto)
        {
            _bikeShopService.Update(id, dto);
            return Ok();
        }
    }
}
