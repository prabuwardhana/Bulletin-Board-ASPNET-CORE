using System.Threading.Tasks;

namespace BulletinBoard.Services.EmailService.Interface
{
    public interface IEmailSenderService
    {
         void SendEmail(Message message);
         Task SendEmailAsync(Message message);
    }
}