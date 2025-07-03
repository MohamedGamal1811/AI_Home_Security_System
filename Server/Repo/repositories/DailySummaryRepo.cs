using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Server.Date;
using Server.Models.Entities;
using Server.Repo.interfaces;

namespace Server.Repo.repositories
{
    
    public class DailySummaryRepo : IDailySummaryRepo
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailRepo _emailRepo;

        public DailySummaryRepo(ApplicationDbContext context, IEmailRepo emailRepo)
        {
            _context = context;
            _emailRepo = emailRepo;
        }

        public async Task GenerateAndSendSummariesAsync()
        {
            var today = DateTime.UtcNow.Date;
            var users = await _context.UserImages.ToListAsync();
            var user = await _context.UserImages.OrderBy(p=>p.UploadedAt).LastOrDefaultAsync(p=>p.Relation.ToLower()=="father"); 

            var todayImages = await _context.ReceivedImages.
                Where(i=>i.Name == user.Name && i.TimeStamp == today).ToListAsync();


            int known = todayImages.Count(i => i.Classification.ToLower() == "real");
            int unKnown = todayImages.Count(i=> i.Classification.ToLower() == "fake");

            // int sosCount = await _context.AuditLogs // Or SOSLogs if exists
            //.CountAsync(a => a.PerformedByUserId == user.Id
            //              && a.Action.Contains("SOS")
            //              && a.Timestamp.Date == today);

            var summary = new DailySummaryLog
            {
                UserId = user.OwnerUserId,
                Date = today,
                KnownCount = known,
                UnknownCount = unKnown,
                SosCount = 0
            };

            _context.DailySummaryLogs.Add(summary);
            await _context.SaveChangesAsync();
            await _emailRepo.SendDailySummaryEmail("mohamegamal1000@gmail.com",user.Name,summary);

            

        }


    }
}
