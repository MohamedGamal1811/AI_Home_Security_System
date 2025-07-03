using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models.Entities
{
    public class History
    {
        [Key]
        public int Id { get; set; }
        public string image { get; set; }
        public DateTime date { get; set; }
        [MaxLength(50)]
        public string status { get; set; }
        public string name { get; set; }
    }

}
