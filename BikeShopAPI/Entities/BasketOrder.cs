using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class BasketOrder
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string? ProductName { get; set; }
        public int BasketId { get; set; }
        public virtual Basket Basket { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
