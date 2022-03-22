using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("/shop/{shopId}/bag")]
    [ApiController]
    public class BagController : ControllerBase
    {
        private readonly IBagService _bagService;
        public BagController(IBagService bagService)
        {
            _bagService = bagService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<BagDto>> GetAll([FromRoute] int shopId)
        {
            var bags = _bagService.GetAll(shopId);
            return Ok(bags);

        }
        [AllowAnonymous]
        [HttpGet("{bagId}")]
        public ActionResult<BagDto> Get([FromRoute] int shopId, [FromRoute] int bagId)
        {
            var bag = _bagService.Get(shopId, bagId);
            return Ok(bag);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public ActionResult CreateBag([FromRoute] int shopId, [FromBody]CreateBagDto dto)
        {
            var id = _bagService.Create(shopId, dto);
            return Created($"shop/{shopId}/bag/{id}", null);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{bagId}")]
        public ActionResult DeleteBag([FromRoute] int shopId, [FromRoute] int bagId)
        {
            _bagService.Delete(shopId, bagId);
            return NoContent();
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPatch("{bagId}")]
        public ActionResult UpdateBag([FromRoute] int shopId, [FromRoute] int bagId, [FromBody] UpdateBagDto dto)
        {
            _bagService.Update(shopId, bagId, dto);
            return Ok();
        }
    }
}
