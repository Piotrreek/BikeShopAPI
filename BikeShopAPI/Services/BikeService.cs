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
    public class BikeService : IBikeService
    {
        private readonly IBikeShopRepository _bikeShopRepository;
        private readonly IBikeRepository _bikeRepository;
        private readonly IMapper _mapper;
        private readonly IUserContextService _userContextService;
        private readonly IAuthorizationService _authorizationService;
        public BikeService(IBikeShopRepository bikeShopRepository, IBikeRepository bikeRepository, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationService)
        {
            _bikeShopRepository = bikeShopRepository;
            _bikeRepository = bikeRepository;
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationService = authorizationService;
        }
        public List<BikeDto> GetAll(int bikeShopId)
        {
            var bikes = _bikeRepository.GetByShopId(bikeShopId);
            var bikesDto = _mapper.Map<List<BikeDto>>(bikes);
            return bikesDto;
        }
        public BikeDto Get(int id)
        {
            var bike = _bikeRepository.GetById(id);
            var isBikeNull = CheckBikeNull(bike);
            if (isBikeNull == false)
            {
                throw new NotFoundException("Bike not found");
            }
            return _mapper.Map<BikeDto>(bike);
        }
        public int Create(int bikeShopId, CreateBikeDto dto)
        {
            var shop = _bikeShopRepository.GetById(bikeShopId);
            var isShop = CheckShop(shop);
            if (isShop == false)
            {
                throw new NotFoundException("Shop not found");
            }
            var bike = _mapper.Map<Bike>(dto);
            bike.BikeShopId = bikeShopId;
            bike.CreatedById = _userContextService.GetUserId;
            _bikeRepository.Insert(bike);
            _bikeRepository.Save();
            return bike.Id;
        }
        public List<BikeDto> GetAllWithoutId()
        {
            var bikes = _bikeRepository.GetAll();
            var bikesDto = _mapper.Map<List<BikeDto>>(bikes);
            return bikesDto;
        }
        public void Delete(int id)
        {
            var bike = _bikeRepository.GetById(id);
            var isBikeNull = CheckBikeNull(bike);
            if (isBikeNull == false)
            {
                throw new NotFoundException("Bike not found");
            }
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, bike, new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            _bikeRepository.Delete(bike);
            _bikeRepository.Save();
        }
        public void Update(int id, UpdateBikeDto dto)
        {
            var bike = _bikeRepository.GetById(id);
            var isBikeNull = CheckBikeNull(bike);
            if (isBikeNull == false)
            {
                throw new NotFoundException("Bike not found");
            }
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, bike, new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            bike = _mapper.Map(dto, bike);
            _bikeRepository.Update(bike);
            _bikeRepository.Save();
        }
        private bool CheckShop(BikeShop? shop)
        {
            return shop != null;
        }
        private bool CheckBikeNull(Bike? bike)
        {
            return bike != null;
        }
    }
}
