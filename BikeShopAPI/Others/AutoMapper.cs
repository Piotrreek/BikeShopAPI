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
        }
    }
}
