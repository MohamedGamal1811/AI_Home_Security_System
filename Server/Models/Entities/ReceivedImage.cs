using System.ComponentModel.DataAnnotations;

namespace Server.Models.Entities
{
    public class ReceivedImage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [MaxLength(50)]
        public string Classification { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ImagePath { get; set; }
    }
}
