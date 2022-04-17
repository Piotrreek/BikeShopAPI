using System.ComponentModel.DataAnnotations;

namespace BikeShopAPI.Entities
{
    public class Role
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
