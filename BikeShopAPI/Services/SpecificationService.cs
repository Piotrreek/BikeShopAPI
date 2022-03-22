using AutoMapper;
using BikeShopAPI.Authorization;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class SpecificationService : ISpecificationService
    {
        private readonly IMapper _mapper;
        private readonly BikeShopDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public SpecificationService(BikeShopDbContext context, IMapper mapper, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }
        public List<SpecificationDto> GetSpecOfBike(int bikeId)
        {
            var bike = CheckIfThereIsGoodBikeInContext(bikeId);
            var spec = _context.Specifications?
                .Where(s => s.BikeId == bikeId)
                .ToList();
            CheckIfSpecIsNull(spec);
            var specDto = _mapper.Map<List<SpecificationDto>>(spec);
            return specDto;
        }
        public void Create(int bikeId, CreateSpecificationDto dto)
        {
            var bike = CheckIfThereIsGoodBikeInContext(bikeId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, bike,
                new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var spec = _mapper.Map<Specification>(dto);
            spec.BikeId = bikeId;
            spec.CreatedById = _userContextService.GetUserId;
            _context.Specifications?.Add(spec);
            _context.SaveChanges();
        }
        public void Delete(int bikeId)
        {
            var bike = CheckIfThereIsGoodBikeInContext(bikeId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, bike,
                new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var spec = _context.Specifications?
                .Where(s => s.BikeId == bikeId)
                .ToList();
            CheckIfSpecIsNull(spec);
            _context.RemoveRange(spec);
            _context.SaveChanges();
        }
        public void DeleteById(int bikeId, int specId)
        {
            var bike = CheckIfThereIsGoodBikeInContext(bikeId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, bike,
                new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var spec = _context.Specifications?
                .FirstOrDefault(s => s.BikeId == bikeId && s.Id == specId);
            if (spec is null)
            {
                throw new NullSpecificationException("This bike does not have such specification");
            }
            _context.Specifications?.Remove(spec);
            _context.SaveChanges();
        }
        public void Update(int bikeId, int specId, UpdateSpecificationDto dto)
        {
            var bike = CheckIfThereIsGoodBikeInContext(bikeId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, bike,
                new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var spec = _context.Specifications?
                .FirstOrDefault(s => s.BikeId == bikeId && s.Id == specId);
            spec = _mapper.Map(dto, spec);
            _context.SaveChanges();
        }
        private static void CheckIfSpecIsNull(List<Specification>? spec)
        {
            if (spec?.Count == 0)
            {
                throw new NullSpecificationException("This bike does not have any specification assigned");
            }
        }
        private Bike CheckIfThereIsGoodBikeInContext(int bikeId)
        {
            if (_context.Bikes is null)
            {
                throw new NotFoundException("We do not have such a bike");
            }
            var bike = _context.Bikes
                .Include(b => b.Specification)
                .FirstOrDefault(b => b.Id == bikeId);
            if (bike == null)
            {
                throw new NotFoundException("We do not have such a bike");
            }
            return bike;
        }
    }
}
