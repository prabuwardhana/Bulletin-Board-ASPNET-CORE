using System.Threading.Tasks;

namespace Services.EmailService.Interface
{
    public interface IEmailSenderService
    {
         void SendEmail(Message message);
         Task SendEmailAsync(Message message);
    }
}