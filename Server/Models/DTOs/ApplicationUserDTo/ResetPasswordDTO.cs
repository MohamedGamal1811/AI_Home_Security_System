using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.ApplicationUser
{
    public class ResetPasswordDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        [MinLength(6)]
        public string NewPassword { get; set; }
    }
}
