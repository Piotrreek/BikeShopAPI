using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShopAPI.Entities
{
    public class Address
    {
        [ForeignKey("BikeShop")]
        public int Id { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public virtual BikeShop? Shop { get; set; }
    }
}
