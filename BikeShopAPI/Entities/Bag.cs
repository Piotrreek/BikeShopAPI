namespace BikeShopAPI.Entities
{
    public class Bag
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int Count { get; set; }
        public int BikeShopId { get; set; }
        public virtual BikeShop? Shop { get; set; }

    }
}
