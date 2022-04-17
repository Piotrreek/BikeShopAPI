﻿using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace BikeShopAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string EMailAddress { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
