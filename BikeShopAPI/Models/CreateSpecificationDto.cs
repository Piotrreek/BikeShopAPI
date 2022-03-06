namespace BikeShopAPI.Models
{
    public class CreateSpecificationDto
    {
        public string? Brand { get; set; }
        public string? Name { get; set; }
        public int ProductionYear { get; set; }
        public string? Description { get; set; }
    }
}
