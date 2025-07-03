using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        ICollection<UserImage> Images { get; set; }
    }
}