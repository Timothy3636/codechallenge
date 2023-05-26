using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.AspNetCore.Identity;

namespace Api.Models
{

    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(a => a.AdminID);
            builder.Property(a => a.FirstName).IsRequired();
            builder.Property(a => a.LastName).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Password).IsRequired();
            builder.Property(a => a.IsActive);
            builder.Property(a => a.AddedDate);
        }
    }


    public class ApplicationUserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Password).IsRequired();
            builder.Property(u => u.IsActive);
            builder.Property(u => u.RegistrationDate);
        }
    }

    public class EmailNotificationsConfiguration : IEntityTypeConfiguration<EmailNotifications>
    {
        public void Configure(EntityTypeBuilder<EmailNotifications> builder)
        {
            builder.HasKey(en => en.NotificationID);
            builder.Property(en => en.RecipientEmail).IsRequired();
            builder.Property(en => en.EmailSubject);
            builder.Property(en => en.EmailBody);
            builder.Property(en => en.EmailSentDate);
            builder.Property(en => en.IsEmailDelivered);
            builder.Property(en => en.SenderEmail);
            builder.Property(en => en.EmailDeliveredDate);
            builder.Property(en => en.EmailStatus);

        }
    }

    public class UserCashBalanceConfiguration : IEntityTypeConfiguration<UserCashBalance>
    {
        public void Configure(EntityTypeBuilder<UserCashBalance> builder)
        {        
            builder.HasKey(ucb => ucb.BalanceID); // Set the primary key
            builder.Property(ucb => ucb.BalanceID).IsRequired();

            // Configure other properties as needed
            builder.Property(ucb => ucb.CashBalance).HasColumnType("decimal(18,2)").IsRequired();
        }
    }

}
