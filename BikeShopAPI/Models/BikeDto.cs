using BikeShopAPI.Entities;

namespace BikeShopAPI.Models
{
    public class BikeDto
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? ProductionYear { get; set; }
        public int BikeShopId { get; set; }
        public List<SpecificationDto>? Specification { get; set; }
    }
}
