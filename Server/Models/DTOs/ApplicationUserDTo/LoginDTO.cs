using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.ApplicationUser
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]      
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
