using BikeShopAPI.Models;

namespace BikeShopAPI.Interfaces
{
    public interface IUserService
    {
        public void Register(RegisterUserDto dto);
        public void Delete(DeleteUserDto dto);
        public string LoginAndGenerateJwt(LoginDto dto);
    }
}
