using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IUserNotificationRepository
    {
         Task<UserNotification> GetUserNotification(int notificationId, string userId, bool trackChanges = false);
         Task<IEnumerable<UserNotification>> GetUserNotifications(string userId, bool trackChanges);
         void UpdateUserNotification(UserNotification userNotification);
         void CreateUserNotification(UserNotification userNotification);
    }
}