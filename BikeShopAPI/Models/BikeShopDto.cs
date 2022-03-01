namespace BikeShopAPI.Models
{
    public class BikeShopDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Street { get; set; }
    }
}
