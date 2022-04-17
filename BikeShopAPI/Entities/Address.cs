using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BikeShopAPI.Entities
{
    public class Address
    {
        [ForeignKey("BikeShop")]
        public int Id { get; set; }
        [Required]
        [MaxLength(15)]
        public string? City { get; set; }
        [Required]
        [MaxLength(20)]
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public virtual BikeShop? Shop { get; set; }
    }
}
