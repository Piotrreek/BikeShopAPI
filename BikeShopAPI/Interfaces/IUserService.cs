using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IUserService
    {
        public void Register(RegisterUserDto dto);
    }
}
