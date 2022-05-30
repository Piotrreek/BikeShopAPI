using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Repositories
{
    public class BikeRepository : IBikeRepository
    {
        private readonly BikeShopDbContext _context;
        private readonly DbSet<Bike> _table;
        public BikeRepository(BikeShopDbContext context)
        {
            _context = context;
            _table = _context.Set<Bike>();
        }
        public List<Bike>? GetAll()
        {
            return _table
                .AsNoTracking()
                .Include(s => s.Specification)
                .ToList();
        }

        public Bike? GetById(int id)
        {
            return _table
                .AsNoTracking()
                .Include(s => s.Specification)
                .FirstOrDefault(b => b.Id == id);
        }

        public List<Bike>? GetByShopId(int shopId)
        {
            return _context.Bikes?
                .AsNoTracking()
                .Include(s => s.Specification)
                .Where(b => b.BikeShopId == shopId)
                .ToList();
        }

        public void Insert(Bike bike)
        {
            _context.Add(bike);
        }

        public void Update(Bike bike)
        {
            _table.Attach(bike);
            _context.Entry(bike).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Bike bike)
        {
            _context.Remove(bike);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
