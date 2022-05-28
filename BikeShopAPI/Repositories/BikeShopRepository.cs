using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Repositories
{
    public class BikeShopRepository : IBikeShopRepository
    {
        private readonly BikeShopDbContext _context;
        private readonly DbSet<BikeShop> _table;

        public BikeShopRepository(BikeShopDbContext context)
        {
            _context = context;
            _table = _context.Set<BikeShop>();
        }
        public IEnumerable<BikeShop> GetAll()
        {
            return _table
                    .Include(a => a.Address)
                .Include(a => a.Bikes)
                .ToList();
        }
        public BikeShop? GetById(int id)
        {
            return _table
                .Include(a => a.Address)
                .Include(a => a.Bikes)
                .FirstOrDefault(s => s.Id == id);
        }
        public void Insert(BikeShop obj)
        {
            _table.Add(obj);
            _context.SaveChanges();
        }
        public void Update(BikeShop obj)
        {
            _table.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(BikeShop obj)
        {
            _context.Remove(obj);
            _context.SaveChanges();
        }
    }
}
