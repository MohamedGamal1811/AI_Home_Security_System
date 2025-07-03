 using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class House
    {
        [Key]
        public Guid HouseId { get; set; }

        [Required]
        public string Name { get; set; }  

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string ZipCode { get; set; }
        [Required]
        public string Country { get; set; }

        public string Description { get; set; } 
      
        //public string UserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }

     public ICollection<Device> Devices { get; set; }
    }

}
