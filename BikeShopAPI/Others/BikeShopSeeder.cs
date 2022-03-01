using BikeShopAPI.Entities;

namespace BikeShopAPI.Others
{
    public class BikeShopSeeder
    {
        private readonly BikeShopDbContext _dbContext;

        public BikeShopSeeder(BikeShopDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Seed()
        {
            if (_dbContext.Database.CanConnect())
            {
                if (!_dbContext.BikeShops.Any())
                {
                    var shop = GetShop();
                    _dbContext.AddRange(shop);
                    _dbContext.SaveChanges();
                }
            }
        }

        public BikeShop GetShop()
        {
            BikeShop bikeShop = new BikeShop()
            {
                Address = new Address()
                {
                    City = "Krzeszowice",
                    PostalCode = "32-540",
                    Street = "Krakowska 50"
                },
                Name = "Bike Atelier",
                Description = "The most popular Bike Shop in southern Poland",
                ContactEmail = "bikeatelier@bike.com",
                ContactNumber = "123456789",
                Bikes = new List<Bike>()
                {
                    new Bike()
                    {
                        Brand = "Kross",
                        Count = 10,
                        Description = "MTB bike",
                        Name = "Level 8.0",
                        Price = 3999,
                        ProductionYear = "2019",
                        Specification = new List<Specification>()
                        {
                            new Specification()
                            {
                                Brand = "Shimano",
                                Description = "Pedals",
                                Name = "Pedals SPD",
                                ProductionYear = 2019
                            }
                        }
                    }

                },
                Bags = new List<Bag>()
                {
                    new Bag()
                    {
                        Brand = "TopPeak",
                        Count = 10,
                        Description = "Really nice bag",
                        Name = "TopPeak BackLoader",
                        Price = 299
                    }
                },
                Products = new List<Product>()
                {
                    new Product()
                    {
                        Brand = "Isostar",
                        Count = 159,
                        Description = "Water bottle",
                        Name = "Isostar 1L water bottle",
                        Price = 15
                    }
                }
            };
            return bikeShop;
        }
    }
}
