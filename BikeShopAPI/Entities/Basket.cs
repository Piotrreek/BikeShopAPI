using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class Basket
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EMail { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int HouseNumber { get; set; }
        public int? UserId { get; set; }
        public ICollection<BasketOrder> BasketOrders { get; set; }
        public virtual User? User { get; set; }
    }
}
