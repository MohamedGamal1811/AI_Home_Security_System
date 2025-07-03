using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.ApplicationUser
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
