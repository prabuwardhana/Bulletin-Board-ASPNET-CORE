using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Entities.Models;

namespace BulletinBoard.Contracts
{
    public interface IUserNotificationRepository
    {
         Task<UserNotification> GetUserNotificationAsync(int notificationId, string userId, bool trackChanges = false);
         Task<IEnumerable<UserNotification>> GetUserNotificationsAsync(string userId, bool trackChanges);
         void UpdateUserNotification(UserNotification userNotification);
         void CreateUserNotification(UserNotification userNotification);
    }
}