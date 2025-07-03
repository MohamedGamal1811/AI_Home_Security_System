using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Models.Entities;
using static DeviceConfiguration;

namespace Server.Date
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<History> Histories { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Alarm> Alarms { get; set; }
        public DbSet<UserImage> UserImages { get; set; }
        public DbSet<ReceivedImage> ReceivedImages { get; set; }
        public DbSet<DailySummaryLog> DailySummaryLogs { get; set; }
        
         
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply default Identity configurations
            base.OnModelCreating(modelBuilder);

            // Custom configurations for other entities
            modelBuilder.Entity<House>()
                .HasMany(h => h.Devices)
                .WithOne(d => d.House)
                .HasForeignKey(d => d.HouseId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new HouseConfiguration());

        }
    }
}




















