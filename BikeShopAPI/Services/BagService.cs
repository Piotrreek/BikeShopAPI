using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class BagService : IBagService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;

        public BagService(BikeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<BagDto> GetAll(int shopId)
        {
            var shop = CheckIfShopExists(shopId);
            var bags = shop.Bags;
            var bagsDto = _mapper.Map<List<BagDto>>(bags);
            if (bagsDto is null)
            {
                throw new NullSpecificationException("This shop does not have any bags in its offer");
            }
            return bagsDto;
        }

        public BagDto Get(int shopId, int bagId)
        {
            var shop = CheckIfShopExists(shopId);
            var bag = shop.Bags?.FirstOrDefault(b => b.Id == bagId);
            var bagDto = _mapper.Map<BagDto>(bag);
            if (bagDto is null)
            {
                throw new NotFoundException("The bag with given id does not exist");
            }

            return bagDto;
        }

        public int Create(int shopId, CreateBagDto dto)
        {
            CheckIfShopExists(shopId);
            var bag = _mapper.Map<Bag>(dto);
            bag.BikeShopId = shopId;
            _context.Add(bag);
            _context.SaveChanges();
            return bag.Id;
        }

        public void Delete(int shopId, int bagId)
        {
            var shop = CheckIfShopExists(shopId);
            var bag = shop.Bags?.FirstOrDefault(b => b.Id == bagId);
            if (bag is null)
            {
                throw new NotFoundException("The bag with given id does not exist");
            }

            _context.Remove(bag);
            _context.SaveChanges();
        }

        public void Update(int shopId, int bagId, UpdateBagDto dto)
        {
            var shop = CheckIfShopExists(shopId);
            var bag = shop.Bags?.FirstOrDefault(b => b.Id == bagId);
            if (bag is null)
            {
                throw new NotFoundException("The bag with given id does not exist");
            }

            bag = _mapper.Map(dto, bag);
            _context.SaveChanges();
        }

        private BikeShop CheckIfShopExists(int shopId)
        {
            var shop = _context.BikeShops
                .Include(s => s.Bags)
                .FirstOrDefault(s => s.Id == shopId);
            if (shop == null)
            {
                throw new NotFoundException("Shop not found");
            }

            return shop;
        }
    }
}
