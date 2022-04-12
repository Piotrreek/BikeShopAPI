using AutoMapper;
using BikeShopAPI.Entities;
using BikeShopAPI.Models;

namespace BikeShopAPI.Others
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<BikeShop, BikeShopDto>()
                .ForMember(s => s.City, c => c.MapFrom(m => m.Address.City))
                .ForMember(s => s.PostalCode, c => c.MapFrom(m => m.Address.PostalCode))
                .ForMember(s => s.Street, c => c.MapFrom(m => m.Address.Street));
            CreateMap<CreateBikeShopDto, BikeShop>()
                .ForMember(s => s.Address,
                    c => c.MapFrom(dto => new Address()
                    {
                        City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street
                    }));
            CreateMap<UpdateBikeShopDto, BikeShop>();
            CreateMap<Bike, BikeDto>();
            CreateMap<Specification, SpecificationDto>();
            CreateMap<CreateBikeDto, Bike>();
            CreateMap<UpdateBikeDto, Bike>();
            CreateMap<CreateSpecificationDto, Specification>();
            CreateMap<UpdateSpecificationDto, Specification>();
            CreateMap<Bag, BagDto>();
            CreateMap<CreateBagDto, Bag>();
            CreateMap<UpdateBagDto, Bag>();
            CreateMap<Product, ProductDto>();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<BuyNowDto, Order>();
            CreateMap<Order, BasketOrder>();
        }
    }
}
