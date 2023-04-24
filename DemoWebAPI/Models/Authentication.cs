using System.ComponentModel.DataAnnotations;

namespace DemoWebAPI.Models
{
    public class Authentication
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
