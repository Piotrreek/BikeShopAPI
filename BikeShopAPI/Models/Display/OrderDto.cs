namespace BikeShopAPI.Models
{
    public class OrderDto
    {
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public string? ProductName { get; set; }
    }
}
