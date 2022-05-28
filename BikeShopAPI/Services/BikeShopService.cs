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
        private readonly IBikeShopRepository _repository;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationHandler;
        public BikeShopService(IBikeShopRepository repository, IMapper mapper, IUserContextService userContextService, IAuthorizationService authorizationHandler)
        {
            _mapper = mapper;
            _userContextService = userContextService;
            _authorizationHandler = authorizationHandler;
            _repository = repository;
        }
        public BikeShopDto GetById(int id)
        {
            var shop = _repository.GetById(id);
            if (shop == null)
            {
                throw new NotFoundException("Bike shop not found");
            }

            var shopDto = _mapper.Map<BikeShopDto>(shop);
            return shopDto;
        }
        public List<BikeShopDto> GetAll()
        {
            var shops = _repository.GetAll();
            var shopsDto = _mapper.Map<List<BikeShopDto>>(shops);
            return shopsDto;
        }
        public int Create(CreateBikeShopDto dto)
        {
            var bikeShop = _mapper.Map<BikeShop>(dto);
            bikeShop.CreatedById = _userContextService.GetUserId;
            _repository.Insert(bikeShop);
            return bikeShop.Id;
        }
        public void Delete(int id)
        {
            var bikeShopToDelete = _repository.GetById(id);
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
            _repository.Delete(bikeShopToDelete);
        }
        public void Update(int id, UpdateBikeShopDto dto)
        {
            var bikeShopToUpdate = _repository.GetById(id);
            if (bikeShopToUpdate is null)
            {
                throw new NotFoundException("Bike shop not found");
            }
            var authorizationResult = _authorizationHandler.AuthorizeAsync(_userContextService.User, bikeShopToUpdate, new OperationRequirement(Operation.Update)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            bikeShopToUpdate = _mapper.Map(dto, bikeShopToUpdate);
            _repository.Update(bikeShopToUpdate);
        }
    }
}
