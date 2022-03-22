using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("bike")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<BikeDto>> GetAllBikes()
        {
            var bikes = _bikeService.GetAllWithoutId();
            return Ok(bikes);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<BikeDto> GetBike([FromRoute] int id)
        {
            var bike = _bikeService.Get(id);
            return Ok(bike);
        }
        [AllowAnonymous]
        [HttpGet("shop/{bikeShopId}")]
        public ActionResult<List<BikeDto>> GetAllBikesWithGivenBikeShopId([FromRoute]int bikeShopId)
        {
            var bikes = _bikeService.GetAll(bikeShopId);
            return Ok(bikes);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost("shop/{bikeShopId}")]
        public ActionResult CreateBike([FromRoute] int bikeShopId, [FromBody]CreateBikeDto dto)
        {
            var id = _bikeService.Create(bikeShopId, dto);
            return Created($"{id}/shop/{bikeShopId}", null);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public ActionResult DeleteBike([FromRoute] int id)
        {
            _bikeService.Delete(id);
            return NoContent();
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPatch("{id}")]
        public ActionResult UpdateBike([FromRoute] int id, UpdateBikeDto dto)
        {
            _bikeService.Update(id, dto);
            return Ok();
        }
    }
}
