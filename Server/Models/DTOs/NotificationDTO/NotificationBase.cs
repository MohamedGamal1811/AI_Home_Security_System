using Server.Models.Entities;

namespace Server.Models.DTOs.NotificationDTO
{
    public class NotificationBase
    {
     
        public string Type { get; set; }
        public string Message { get; set; }

        public bool? SendEmail { get; set; }
        public bool? SendSMS { get; set; }
        public bool? SendPush { get; set; }
        public bool? IsSent { get; set; }
        public bool? IsRead { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? ReadAt { get; set; }
        public Guid? DeviceId { get; set; }
        public Guid? HouseId { get; set; }

    }
}
