using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Notification
    {
       [Key]
       public Guid Id { get; set; }

        [Required]
        public string Type { get; set; }

        [Required]
        public string Message { get; set; }

        //way the notification sent to user not needed
        public bool SendEmail { get; set; }
        public bool SendSMS { get; set; }
        public bool SendPush { get; set; }

        //check reached & read or not 
        public bool IsSent { get; set; }
        public bool IsRead { get; set; }


        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }

        [ForeignKey("DeviceId")]
        public Guid? DeviceId { get; set; }
        public Device Device { get; set; }

        [ForeignKey("HouseId")]
        public Guid? HouseId { get; set; }
        public House House { get; set; }
        

        //here we need user id info
    }

}
