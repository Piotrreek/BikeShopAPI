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
            var isBike = CheckIfThereIsGoodBikeInContext(bikeId);
            var bike = _context.Bikes?
                .FirstOrDefault(b => b.Id == bikeId);
            var spec = _context.Specifications?
                .Where(s => s.BikeId == bikeId)
                .ToList();
            var isSpec = CheckIfSpecIsNull(spec);
            if (isSpec == false)
            {
                throw new NullSpecificationException("This bike does not have any specification assigned");
            }
            var specDto = _mapper.Map<List<SpecificationDto>>(spec);
            return specDto;
        }
        public void Create(int bikeId, CreateSpecificationDto dto)
        {
            var isBike = CheckIfThereIsGoodBikeInContext(bikeId);
            var bike = _context.Bikes?
                .Include(b => b.Specification)
                .FirstOrDefault(b => b.Id == bikeId);
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
            var isBike = CheckIfThereIsGoodBikeInContext(bikeId);
            var bike = _context.Bikes?
                .Include(b => b.Specification)
                .FirstOrDefault(b => b.Id == bikeId);
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, bike,
                new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var spec = _context.Specifications?
                .Where(s => s.BikeId == bikeId)
                .ToList();
            var isSpec = CheckIfSpecIsNull(spec);
            if (isSpec == false)
            {
                throw new NullSpecificationException("This bike does not have any specification assigned");
            }
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
        private static bool CheckIfSpecIsNull(List<Specification>? spec)
        {
            return spec?.Count > 0;
        }
        private bool CheckIfThereIsGoodBikeInContext(int bikeId)
        {
            var bike = _context.Bikes?
                .FirstOrDefault(b => b.Id == bikeId);
            return bike != null;
        }
    }
}
