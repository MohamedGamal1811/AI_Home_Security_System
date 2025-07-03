using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class UsersImage
    {
        [Key]
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name  { get; set; }

        [StringLength(255)]
        [Base64String]
        // URL to the user's profile image
        public string ImageUrl { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}