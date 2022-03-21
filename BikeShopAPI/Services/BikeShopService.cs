using AutoMapper;
using BikeShopAPI.Authorization;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class BikeShopService : IBikeShopService
    {
        private readonly IUserContextService _userContextService;
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationHandler;
        public BikeShopService(BikeShopDbContext context, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationHandler)
        {
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationHandler = authorizationHandler;
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
            bikeShop.CreatedById = _userContextService.GetUserId;
            _context.Add(bikeShop);
            _context.SaveChanges();
            return bikeShop.Id;
        }

        public void Delete(int id)
        {
            var bikeShopToDelete = _context.BikeShops
                .FirstOrDefault(s => s.Id == id);
            if (bikeShopToDelete is null)
            {
                throw new NotFoundException("Bike shop not found");
            }
            var authorizationResult = _authorizationHandler.AuthorizeAsync(_userContextService.User, bikeShopToDelete,
                new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var addressToDelete = _context.Addresses?.FirstOrDefault(a => a.Id == bikeShopToDelete.AddressId);
            _context.Remove(addressToDelete);
            _context.Remove(bikeShopToDelete);
            _context.SaveChanges();
        }

        public void Update(int id, UpdateBikeShopDto dto)
        {
            var bikeShopToUpdate = _context.BikeShops
                .FirstOrDefault(s => s.Id == id);
            if (bikeShopToUpdate is null)
            {
                throw new NotFoundException("Bike shop not found");
            }
            bikeShopToUpdate = _mapper.Map(dto, bikeShopToUpdate);
            _context.SaveChanges();
        }
    }
}
