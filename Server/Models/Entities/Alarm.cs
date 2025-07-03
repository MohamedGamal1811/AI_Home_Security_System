using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Alarm
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string TriggerType { get; set; }
        [Required]
        public DateTime TriggeredAt{ get; set; }
        [Required]
        public string Severity { get; set; }    //low ,meduim,high
        
        public bool IsFalseAlarm { get; set; }
        public bool AutoResponseTriggered { get; set; }
         
      

        // Navigation Property
        [ForeignKey("HouseId")]
        public House House { get; set; }
        public Guid? HouseId { get; set; }
        
        [ForeignKey("NotificationId")]
        public Notification Notification { get; set; }
        public Guid? NotificationId { get; set; }
        

        
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }
        public Guid? DeviceId { get; set; }
    }
}
