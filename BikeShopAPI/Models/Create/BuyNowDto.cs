using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Models
{
    public class BuyNowDto
    {
        [Required]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? EMail { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Street { get; set; }
        [Required]
        public int HouseNumber { get; set; }
    }
}
