namespace Server.Models.DTOs.DeviceDTo
{
    public class DeviceBaseDTO
    {
       
        public string DeviceName { get; set; }
        public string DeviceType { get; set; }
        public string Status { get; set; }
        public Guid? HouseId { get; set; }
    }
}
