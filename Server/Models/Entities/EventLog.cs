using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class EventLog
    {
        [Key]
        public int EventId { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required, StringLength(100)]
        public string EventType { get; set; } // Motion Detected, Door Opened

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string Details { get; set; }

        // Navigation Property
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }

}
