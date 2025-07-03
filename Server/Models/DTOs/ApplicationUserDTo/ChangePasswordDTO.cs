

using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.ApplicationUser
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword   { get; set; }   
        [Required]
        public string NewPassword { get; set; }

    }
}
