namespace Server.Models.DTOs.HistoryDTO
{
    public class CreateHistoryDto : HistoryBaseDto 
    {
        public IFormFile FileURl { get; set; }
    }

}