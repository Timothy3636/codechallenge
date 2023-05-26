using Microsoft.EntityFrameworkCore;
using Api.Models;

namespace Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Admin> Admins { get; set; }        
        
        public DbSet<Contract> Contracts { get; set; }
        
        public DbSet<UserCashBalance> UserCashBalance { get; set; }
        public DbSet<EmailNotifications> EmailNotifications { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
            modelBuilder.ApplyConfiguration(new EmailNotificationsConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new UserCashBalanceConfiguration());
            
            // Tim: On purpose state here, you can take a look for all relationship at 1 glance


            modelBuilder.Entity<Contract>()
                .HasOne(c => c.User)
                .WithMany(u => u.Contracts);                                

            modelBuilder.Entity<UserCashBalance>()
                .HasOne(ccb => ccb.User)
                .WithMany(u => u.UserCashBalances)
                .HasForeignKey(ccb => ccb.UserID);

            modelBuilder.Entity<EmailNotifications>()
                .HasOne(en => en.Contract)
                .WithOne(c => c.EmailNotifications)
                .HasForeignKey<EmailNotifications>(en => en.ContractID);

            base.OnModelCreating(modelBuilder);
        }
    }

}