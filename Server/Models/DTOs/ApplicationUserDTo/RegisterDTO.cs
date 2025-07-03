using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.ApplicationUser
{
    public class RegisterDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        //[Phone]
        [RegularExpression(@"^(\+2)?01[0125][0-9]{8}$",ErrorMessage ="Invalid Egyptian Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
