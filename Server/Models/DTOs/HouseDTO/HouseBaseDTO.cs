namespace Server.Models.DTOs.HouseDTO
{
    public class HouseBaseDTO
    {
    
        public string Name { get; set; } // Example: "John's Home"
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
