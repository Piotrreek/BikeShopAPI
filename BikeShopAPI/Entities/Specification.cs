using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class Specification
    {
        public int Id { get; set; }
        [Required]
        public string? Brand { get; set; }
        [Required]
        public string? Name { get; set; }
        public int ProductionYear { get; set; }
        public string? Description { get; set; }
        public int BikeId { get; set; }
        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual Bike? Bike { get; set; }
    }
}
