using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Entities
{
    public class BikeShopDbContext : DbContext
    {
        private string _connectionString = "Server=(localdb)\\mssqllocaldb;Database=BikeShopDb;Trusted_Connection=True;";
        public DbSet<BikeShop>? BikeShops { get; set; }
        public DbSet<Bike>? Bikes { get; set; }
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Bag>? Bags { get; set; }
        public DbSet<Product>? Products { get; set; }
        public DbSet<Specification>? Specifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BikeShop>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Bike>()
                .Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(15);
            modelBuilder.Entity<Bike>()
                .Property(b => b.Brand)
                .IsRequired()
                .HasMaxLength(15);
            modelBuilder.Entity<Bike>()
                .Property(b => b.Price)
                .IsRequired()
                .HasMaxLength(6)
                .HasPrecision(2);
            modelBuilder.Entity<Bike>()
                .Property(b => b.Count)
                .IsRequired()
                .HasMaxLength(5);
            modelBuilder.Entity<Address>()
                .Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(20);
            modelBuilder.Entity<Address>()
                .Property(a => a.City)
                .IsRequired()
                .HasMaxLength(15);
            modelBuilder.Entity<Bag>()
                .Property(b => b.Name)
                .IsRequired();
            modelBuilder.Entity<Bag>()
                .Property(b => b.Brand)
                .IsRequired();
            modelBuilder.Entity<Bag>()
                .Property(b => b.Count)
                .IsRequired();
            modelBuilder.Entity<Bag>()
                .Property(b => b.Price)
                .IsRequired()
                .HasPrecision(2);
            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Count)
                .IsRequired();
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired()
                .HasPrecision(2);
                
            modelBuilder.Entity<Product>()
                .Property(p => p.Brand)
                .IsRequired();
            modelBuilder.Entity<Specification>()
                .Property(s => s.Brand)
                .IsRequired();
            modelBuilder.Entity<Specification>()
                .Property(s => s.Name)
                .IsRequired();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
