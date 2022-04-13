using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Models
{
    public class BuyNowDto
    {
        public string? PhoneNumber { get; set; }
        public string? EMail { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int HouseNumber { get; set; }
    }
}
