using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class EmergencyContact
    {
        [Key]
        public int ContactId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [Required, Phone]
        public string PhoneNumber { get; set; }

        public string Relation { get; set; }

        // Navigation Property
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }

}
