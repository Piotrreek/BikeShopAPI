using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class Bike
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Brand { get; set; }
        [Required]
        [MaxLength(15)]
        public string? Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        [MaxLength(5)]
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? ProductionYear { get; set; }
        public int BikeShopId { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual List<Specification>? Specification { get; set; }
        public virtual BikeShop? Shop { get; set; }
    }
}
