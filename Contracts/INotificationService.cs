using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface INotificationService
    {
         Task NotifyMsgReceiverAsync (Notification notification, string userId);
         Task<IEnumerable<UserNotification>> GetAllAssignedNotificationAsync(string userName);
         void ReadNotification(int notificationId, string userName);
    }
}