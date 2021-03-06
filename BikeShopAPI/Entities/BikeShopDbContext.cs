using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Entities
{
    public class BikeShopDbContext : DbContext
    {
        public DbSet<BikeShop> BikeShops { get; set; }
        public DbSet<Bike>? Bikes { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Bag>? Bags { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Specification>? Specifications { get; set; }
        public DbSet<User> ? Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketOrder> BasketOrders { get; set; }
        public BikeShopDbContext(DbContextOptions<BikeShopDbContext> options) : base(options) {}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entityType.ClrType.GetProperties().Where(p => p.PropertyType == typeof(decimal));
                foreach (var property in properties)
                {
                    modelBuilder.Entity(entityType.Name).Property(property.Name).HasColumnType("decimal(18,2)");
                }
            }
        }
    }
}
