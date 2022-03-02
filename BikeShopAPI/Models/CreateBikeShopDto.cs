using BikeShopAPI.Entities;

namespace BikeShopAPI.Models
{
    public class CreateBikeShopDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
