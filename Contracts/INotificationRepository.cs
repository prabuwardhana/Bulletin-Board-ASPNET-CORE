using Entities.Models;

namespace Contracts
{
    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
    }
}