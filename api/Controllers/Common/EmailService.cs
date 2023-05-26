namespace Api.Controllers.Common;

using Api.Models;
using Api.Data;
using Api.Helper;

public interface IEmailService
{
    Task SendContractSubmissionEmail(Contract contract);
}

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly IEmailSender _emailSender;
    private readonly ApplicationDbContext _dbContext;

    public EmailService(IConfiguration configuration, IEmailSender emailSender, ApplicationDbContext dbContext)
    {
        _configuration = configuration;
        _emailSender = emailSender;
        _dbContext = dbContext;
    }

    public async Task SendContractSubmissionEmail(Contract contract)
    {

        Console.WriteLine("SendContractSubmissionEmail start");
        
        string ToAddress = _configuration.GetSection("EmailSettings:ToAddress").Value;

        Console.WriteLine("SendContractSubmissionEmail ToAddress="+ToAddress);

        string FromAddress = _configuration.GetSection("EmailSettings:FromAddress").Value;

        Console.WriteLine("SendContractSubmissionEmail FromAddress="+FromAddress);


        string subject = _configuration.GetSection("EmailSettings:DefaultSubject").Value;

        Console.WriteLine("SendContractSubmissionEmail subject="+subject);

        string emailBodyFormat = _configuration.GetSection("EmailSettings:NewContractEmailBodyFormat").Value;

        string body = string.Format(emailBodyFormat, contract.ContractID, contract.FundFromCurrency, 
            contract.FundFromAmount, contract.ConvertToCurrency, contract.ConvertToAmount, contract.ExchangeRate);


        Console.WriteLine("SendContractSubmissionEmail body="+body);        

        // Send the email
        await _emailSender.SendEmailAsync(ToAddress, subject , body);

        // Add the email notification to the database
        EmailNotifications emailNotification = new EmailNotifications
        {
            ContractID = contract.ContractID,
            RecipientEmail = ToAddress,
            EmailSubject = subject,
            EmailBody = body,
            EmailSentDate = DateTime.UtcNow,
            IsEmailDelivered = true,
            SenderEmail = FromAddress,
            EmailDeliveredDate = DateTime.UtcNow,
            EmailStatus = Constant.DeliveredEmailStatus
        };

        // Add the email notification to the database
        _dbContext.EmailNotifications.Add(emailNotification);

        // Mark the contract with the email notification
        contract.EmailNotifications = emailNotification;
        await _dbContext.SaveChangesAsync();
    }
}
