using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class AccessLog
    {
        [Key]
        public int LogId { get; set; }

        [Required]
        public string UserId { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [Required, StringLength(50)]
        public string IpAddress { get; set; }

        [Required]
        public bool Success { get; set; }

        // Navigation Property
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }

}
