using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class BikeShop
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactNumber { get; set; }
        public int? AddressId { get; set; }
        public int? CreatedById { get; set; }
        public virtual User? CreatedBy { get; set; }
        public virtual Address? Address { get; set; } = null;
        public virtual List<Bike>? Bikes { get; set; }
        public virtual List<Product>? Products { get; set; }
        public virtual List<Bag>? Bags { get; set; }
    }
}
