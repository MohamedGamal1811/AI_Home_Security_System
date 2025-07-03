using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Camera
    {
        [Key]
        public int CameraId { get; set; }

        [Required]
        public int DeviceId { get; set; }

        [StringLength(50)]
        public string Resolution { get; set; }

        [Required, StringLength(50)]
        public string IpAddress { get; set; }

        public bool RecordingStatus { get; set; } = false;

        // Navigation Property
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
    }

}
