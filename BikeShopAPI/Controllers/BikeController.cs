using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Route("bike")]
    [ApiController]
    public class BikeController : ControllerBase
    {
        private readonly IBikeService _bikeService;
        public BikeController(IBikeService bikeService)
        {
            _bikeService = bikeService;
        }

        [HttpGet]
        public ActionResult<List<BikeDto>> GetAllBikes()
        {
            var bikes = _bikeService.GetAllWithoutId();
            return Ok(bikes);
        }
        [HttpGet("{id}")]
        public ActionResult<BikeDto> GetBike([FromRoute] int id)
        {
            var bike = _bikeService.Get(id);
            return Ok(bike);
        }
        [HttpGet("shop/{bikeShopId}")]
        public ActionResult<List<BikeDto>> GetAllBikesWithGivenBikeShopId([FromRoute]int bikeShopId)
        {
            var bikes = _bikeService.GetAll(bikeShopId);
            return Ok(bikes);
        }
        [HttpPost("shop/{bikeShopId}")]
        public ActionResult CreateBike([FromRoute] int bikeShopId, [FromBody]CreateBikeDto dto)
        {
            var id = _bikeService.Create(bikeShopId, dto);
            return Created($"{id}/shop/{bikeShopId}", null);
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteBike([FromRoute] int id)
        {
            _bikeService.Delete(id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateBike([FromRoute] int id, UpdateBikeDto dto)
        {
            _bikeService.Update(id, dto);
            return Ok();
        }
    }
}
