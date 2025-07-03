using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Sensor
    {
        [Key]
        public int SensorId { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [Required, StringLength(50)]
        public string SensorType { get; set; } // Motion, Door, Window

        [Required, StringLength(50)]
        public string Status { get; set; } // Active, Inactive

        public DateTime? LastTriggered { get; set; }

        // Navigation Property
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }

}
