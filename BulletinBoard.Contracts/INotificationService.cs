using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Entities.Models;

namespace BulletinBoard.Contracts
{
    public interface INotificationService
    {
         Task NotifyMsgReceiverAsync (Notification notification, string userId);
         Task<IEnumerable<UserNotification>> GetAllAssignedNotificationAsync(string userName);
         Task ReadNotificationAsync(int notificationId, string userName);
    }
}