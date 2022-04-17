using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Brand { get; set; }
        public string? Description { get; set; }
        [Required]
        public decimal? Price { get; set; }
        [Required]
        public int Count { get; set; }
        public int BikeShopId { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual BikeShop? Shop { get; set; }
    }
}
