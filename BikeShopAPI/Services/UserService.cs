using BikeShopAPI.Entities;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace BikeShopAPI.Services
{
    public class UserService : IUserService
    {
        private readonly BikeShopDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        public UserService(BikeShopDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }
        public void Register(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                EMailAddress = dto.EMailAddress,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId,
                UserName = dto.UserName
            };
            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.Password = hashedPassword;
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}
