namespace Server.Repo.interfaces
{
    public interface IDailySummaryRepo
    {
        Task GenerateAndSendSummariesAsync();
    }
}
