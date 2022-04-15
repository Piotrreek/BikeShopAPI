namespace BikeShopAPI.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public string? ProductName { get; set; }
        public DateTime CreatedTime { get; set; }
        public int? ShopCreatorId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EMail { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int HouseNumber { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
    }
}
