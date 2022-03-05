namespace BikeShopAPI.Models
{
    public class CreateBikeDto
    {
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? ProductionYear { get; set; }
    }
}
