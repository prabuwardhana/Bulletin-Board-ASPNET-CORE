using BulletinBoard.Entities.Models;

namespace BulletinBoard.Contracts
{
    public interface INotificationRepository
    {
        void CreateNotification(Notification notification);
    }
}