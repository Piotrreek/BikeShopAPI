﻿using BikeShopAPI.Interfaces;
using BikeShopAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAPI.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _userService.Register(dto);
            return Ok();
        }
        [HttpDelete("delete")]
        public ActionResult DeleteUser([FromBody]DeleteUserDto dto)
        {
            _userService.Delete(dto);
            return Ok();
        }
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            var token = _userService.LoginAndGenerateJwt(dto);
            return Ok(token);
        }
    }
}
