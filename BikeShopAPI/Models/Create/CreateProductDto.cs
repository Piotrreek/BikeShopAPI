using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Models
{
    public class CreateProductDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Brand { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Count { get; set; }
    }
}
