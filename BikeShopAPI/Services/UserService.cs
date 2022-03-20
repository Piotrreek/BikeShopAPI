using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BikeShopAPI.Authorization;
using BikeShopAPI.Entities;
using BikeShopAPI.Exceptions;
using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using BikeShopAPI.Others;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BikeShopAPI.Services
{
    public class UserService : IUserService
    {
        private readonly BikeShopDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly  IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        public UserService(BikeShopDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationSettings authenticationSettings, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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
        public void Delete(DeleteUserDto dto)
        {
            var user = _context.Users?.FirstOrDefault(u => 
                u.UserName == dto.UserName &&
                u.EMailAddress == dto.EMailAddress);
            if (user is null)
            {
                throw new BadRequestException("Wrong username, password or E-mail");
            }
            var authorizationResult = _authorizationService
                .AuthorizeAsync(_userContextService.User, user, new OperationRequirement(Operation.Delete)).Result;
            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }
            var checkPassword = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (checkPassword == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Wrong username, password or E-mail");
            }
            _context.Remove(user);
            _context.SaveChanges();
        }
        public string LoginAndGenerateJwt(LoginDto dto)
        {
            var user = _context.Users?
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserName == dto.UserName);
            if (user is null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim(ClaimTypes.Email, $"{user.EMailAddress}")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);
            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }
    }
}
