using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("bike/{bikeId}/spec")]
    [ApiController]
    public class SpecificationController : ControllerBase
    {
        private readonly ISpecificationService _specificationService;
        public SpecificationController(ISpecificationService specificationService)
        {
            _specificationService = specificationService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<SpecificationDto>> GetWholeSpecificationOfBike([FromRoute] int bikeId)
        {
            var spec = _specificationService.GetSpecOfBike(bikeId);
            return Ok(spec);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public ActionResult CreateSpecification([FromRoute] int bikeId, [FromBody]CreateSpecificationDto dto)
        {
            _specificationService.Create(bikeId, dto);
            return Created($"bike/{bikeId}/spec", null);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete]
        public ActionResult DeleteSpecification([FromRoute] int bikeId)
        {
            _specificationService.Delete(bikeId);
            return NoContent();
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{specId}")]
        public ActionResult DeleteSpecificationById([FromRoute] int bikeId, [FromRoute] int specId)
        {
            _specificationService.DeleteById(bikeId, specId);
            return NoContent();
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPatch("{specId}")]
        public ActionResult UpdateSpecification([FromRoute] int bikeId, [FromRoute] int specId, UpdateSpecificationDto dto)
        {
            _specificationService.Update(bikeId, specId, dto);
            return Ok();
        }
    }
}
