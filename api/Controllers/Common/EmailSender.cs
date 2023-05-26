using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace Api.Controllers.Common
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string recipientEmail, string subject, string body);
    }

    public class EmailSender : IEmailSender
    {

        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string recipientEmail, string subject, string body)
        {
    
            Console.WriteLine("SendEmailAsync recipientEmail="+recipientEmail+";subject="+subject+";body="+body);

            // Retrieve email settings from configuration
            string smtpServer = _configuration.GetSection("EmailSettings:SmtpServer").Value;
            int port = int.Parse(_configuration.GetSection("EmailSettings:Port").Value);
            bool useSsl = bool.Parse(_configuration.GetSection("EmailSettings:UseSsl").Value);
            string fromAddress = _configuration.GetSection("EmailSettings:FromAddress").Value;
            string username = _configuration.GetSection("EmailSettings:Username").Value;
            string password = _configuration.GetSection("EmailSettings:Password").Value;

            Console.WriteLine("Start to send Email");

            try            
            {
                // Create and configure the SmtpClient
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, port))
                {
                    smtpClient.EnableSsl = useSsl;
                    smtpClient.Credentials = new NetworkCredential(username, password);

                    // Create the MailMessage
                    MailAddress fromMailAddress = new MailAddress(fromAddress);
                    MailAddress toMailAddress = new MailAddress(recipientEmail);                
                    MailMessage mailMessage = new MailMessage(fromMailAddress, toMailAddress);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    // Send the email
                    await smtpClient.SendMailAsync(mailMessage);
                }

                Console.WriteLine("Finished to send Email end");
            }
            catch (Exception ex)
            {
                // Handle the exception
                Console.WriteLine("An error occurred while sending the email: " + ex.Message);
            }                
        }
    }
}
