using System.ComponentModel.DataAnnotations;

namespace Server.Models.DTOs.HistoryDTO
{
    public class HistoryBaseDto
    {
        public string image { get; set; }
       // public DateTime Date { get; set; }
        public DateTime date { get; set; }
        public string status { get; set; }
        public string name { get; set; }
    }

}