using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BikeShopAPI.Services
{
    public class SpecificationService : ISpecificationService
    {
        private readonly IMapper _mapper;
        private readonly BikeShopDbContext _context;
        public SpecificationService(BikeShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            CheckIfThereIsGoodBikeInContext(bikeId);
            var spec = _mapper.Map<Specification>(dto);
            spec.BikeId = bikeId;
            _context.Specifications?.Add(spec);
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
