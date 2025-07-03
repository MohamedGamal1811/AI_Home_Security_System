using Server.Repo.interfaces;
using SendGrid;
using Server.Models.Entities;
using SendGrid.Helpers.Mail;
namespace Server.Repo.repositories
{
    public class EmailRepo : IEmailRepo
    {
        private readonly string _apiKey;
        private readonly string _senderEmail;
        private readonly string _senderName;

        public EmailRepo(IConfiguration config)
        {
            _apiKey = config["SendGrid:ApiKey"];
            _senderEmail = config["SendGrid:SenderEmail"];
            _senderName = config["SendGrid:SenderName"];
        }

        public async Task SendDailySummaryEmail(string toEmail, string toName, DailySummaryLog summary)
        {
            var client = new SendGridClient(_apiKey);
            var from = new EmailAddress(_senderEmail, _senderName);
            var to = new EmailAddress(toEmail, toName);

            var subject = $"Your Daily Security Summary - {summary.Date:MMMM dd}";

            var htmlContent = $@"
            <h2>Hello {toName},</h2>
            <p>Here's your security summary for <b>{summary.Date:MMMM dd}</b>:</p>
            <ul>
                <li>✅ Known Faces Recognized: <b>{summary.KnownCount}</b></li>
                <li>❓ Unknown Visitors: <b>{summary.UnknownCount}</b></li>
                <li>🚨 SOS Triggered: <b>{summary.SosCount}</b></li>
            </ul>
            <p>Stay safe,<br/>Your AI Security System</p>";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, null, htmlContent);
            var response = await client.SendEmailAsync(msg);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Body.ReadAsStringAsync();
                throw new Exception($"SendGrid failed: {response.StatusCode} - {error}");
            }

        }
    }
}
