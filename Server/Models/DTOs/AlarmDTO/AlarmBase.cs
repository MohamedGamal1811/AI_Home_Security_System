namespace Server.Models.DTOs.AlarmDTO
{
    public class AlarmBase
    {
        public string TriggerType { get; set; }
        public Guid DeviceId { get; set; }
        public Guid HouseId { get; set; }
        public string Severity { get; set; }
    }

}
