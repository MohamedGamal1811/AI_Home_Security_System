using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class Device
    {
        [Key]
        public Guid DeviceId { get; set; }

        [Required]
        public string DeviceName { get; set; }

        [Required]
        public string DeviceType { get; set; }

        [Required]
        public string Status { get; set; }


        //public string UserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

        // Foreign Key to House (Device belongs to a house)
        [ForeignKey(nameof(House))]
        public Guid HouseId { get; set; }
        public House House { get; set; }
    }

}
