using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Authorize]
    [Route("/shop/{shopId}/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<List<ProductDto>> GetAll([FromRoute] int shopId)
        {
            var products = _productService.GetAll(shopId);
            return Ok(products);
        }
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<ProductDto> Get([FromRoute] int shopId, [FromRoute] int id)
        {
            var product = _productService.Get(shopId, id);
            return Ok(product);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPost]
        public ActionResult Create([FromRoute] int shopId, [FromBody] CreateProductDto dto)
        {
            var id = _productService.Create(shopId, dto);
            
            return Created($"shop/{shopId}/product/{id}", null);
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int shopId, [FromRoute] int id)
        {
            _productService.Delete(shopId, id);
            return NoContent();
        }
        [Authorize(Roles = "Manager,Admin")]
        [HttpPatch("{id}")]
        public ActionResult Update([FromRoute] int shopId, [FromRoute] int id, [FromBody]UpdateProductDto dto)
        {
            _productService.Update(shopId, id, dto);
            return Ok();
        }
    }
}
