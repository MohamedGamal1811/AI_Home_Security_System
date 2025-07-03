using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class UserSetting
    {
        [Key]
        public int SettingId { get; set; }

        [Required]
        public string UserId { get; set; }

        public string NotificationPreferences { get; set; }

        public TimeSpan AutoArmTime { get; set; }

        // Navigation Property
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }

}
