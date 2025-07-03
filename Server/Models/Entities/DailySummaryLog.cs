namespace Server.Models.Entities
{
    public class DailySummaryLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public DateTime Date { get; set; }

        public int KnownCount { get; set; }
        public int UnknownCount { get; set; }
        public int SosCount { get; set; } = 0;
    }
}
