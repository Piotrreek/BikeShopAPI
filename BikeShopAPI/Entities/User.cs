using System.Net.Mail;
using Microsoft.AspNetCore.Identity;

namespace BikeShopAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string EMailAddress { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
