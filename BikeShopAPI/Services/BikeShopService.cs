using System.Globalization;
using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class BikeShopService : IBikeShopService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        public BikeShopService(BikeShopDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public BikeShopDto GetById(int id)
        {
            var shop = _context
                .BikeShops
                .Include(s => s.Address)
                .Include(s => s.Bikes)
                .FirstOrDefault(s => s.Id == id);
            if (shop == null)
            {
                throw new NotFoundException("Bike shop not found");
            }

            var shopDto = _mapper.Map<BikeShopDto>(shop);
            return shopDto;
        }

        public List<BikeShopDto> GetAll()
        {
            var shops = _context
                .BikeShops
                .Include(s => s.Address)
                .Include(s => s.Bikes)
                .ToList();
            if (shops is null)
            {
                throw new NotFoundException("Bike shops not found");
            }

            var shopsDto = _mapper.Map<List<BikeShopDto>>(shops);
            return shopsDto;
        }

        public int Create(CreateBikeShopDto dto)
        {
            var bikeShop = _mapper.Map<BikeShop>(dto);
            _context.Add(bikeShop);
            _context.SaveChanges();
            return bikeShop.Id;
        }
    }
}
