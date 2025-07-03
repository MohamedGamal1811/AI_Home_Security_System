using Server.Models.Entities;

namespace Server.Repo.interfaces
{
    public interface IEmailRepo
    {
        Task SendDailySummaryEmail(string toEmail, string toName, DailySummaryLog summary);
    }
}
