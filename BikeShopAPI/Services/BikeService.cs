﻿using AutoMapper;
using BikeShopAPI.Authorization;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class BikeService : IBikeService
    {
        private readonly BikeShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public BikeService(BikeShopDbContext context, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }
        public List<BikeDto> GetAll(int bikeShopId)
        {
            var shop = _context.BikeShops
                .FirstOrDefault(s => s.Id == bikeShopId);
            CheckShop(shop);
            CheckBike();
            var allBikes = _context.Bikes?
                .Include(b => b.Specification);
            var bikes = allBikes?
                .Where(b => b.BikeShopId == bikeShopId)
                .ToList();
            var bikesDto = _mapper.Map<List<BikeDto>>(bikes);
            return bikesDto;
        }
        public BikeDto Get(int id)
        {
            CheckBike();
            var bike = _context.Bikes?
                .Include(b => b.Specification)
                .FirstOrDefault(b => b.Id == id);
            CheckBikeNull(bike);
            return _mapper.Map<BikeDto>(bike);
        }
        public int Create(int bikeShopId, CreateBikeDto dto)
        {
            var shop = _context.BikeShops
                .FirstOrDefault(s => s.Id == bikeShopId);
            CheckShop(shop);
            var bike = _mapper.Map<Bike>(dto);
            bike.BikeShopId = bikeShopId;
            bike.CreatedById = _userContextService.GetUserId;
            _context.Bikes?.Add(bike);
            _context.SaveChanges();
            return bike.Id;
        }
        public List<BikeDto> GetAllWithoutId()
        {
            CheckBike();
            var bikes = _context?.Bikes?
                .Include(b => b.Specification)
                .ToList();
            var bikesDto = _mapper.Map<List<BikeDto>>(bikes);
            return bikesDto;
        }
        public void Delete(int id)
        {
            var bike = _context.Bikes?
                .FirstOrDefault(b => b.Id == id);
            CheckBikeNull(bike);
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, bike, new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            _context?.Bikes?.Remove(bike);
            _context?.SaveChanges();
        }
        public void Update(int id, UpdateBikeDto dto)
        {
            var bike = _context.Bikes?
                .FirstOrDefault(b => b.Id == id);
            CheckBikeNull(bike);
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, bike, new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            bike = _mapper.Map(dto, bike);
            _context?.SaveChanges();
        }
        private void CheckShop(BikeShop? shop)
        {
            if (shop is null)
            {
                throw new NotFoundException("Shop not found");
            }
        }
        private void SearchBikeShop(int bikeShopId)
        {
            var shop = _context.BikeShops
                .FirstOrDefault(s => s.Id == bikeShopId);
            if (shop is null)
            {
                throw new NotFoundException("Shop not found");
            }
        }
        private void CheckBike()
        {
            if (_context.Bikes is null)
            {
                throw new NotFoundException("There aren't any bikes in database");
            }
        }
        private void CheckBikeNull(Bike? bike)
        {
            if (bike == null)
            {
                throw new NotFoundException("We do not have such a bike");
            }
        }
    }
}
