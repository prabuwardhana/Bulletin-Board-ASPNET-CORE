using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class UserNotificationRepository : RepositoryBase<UserNotification>, IUserNotificationRepository
    {
        public UserNotificationRepository(RepositoryContext repositorycontext) : base(repositorycontext)
        {

        }

        public async Task<UserNotification> GetUserNotificationAsync(int notificationId, string userId, bool trackChanges) =>
            await FindByCondition(n => n.UserId.Equals(userId) && n.NotificationId == notificationId, trackChanges)
                    .FirstOrDefaultAsync();

        public async Task<IEnumerable<UserNotification>> GetUserNotificationsAsync(string userId, bool trackChanges) =>
            await FindByCondition(un => un.UserId.Equals(userId) && !un.IsRead, trackChanges)
                    .Include(un => un.Notification)
                    .ToListAsync();

        public void UpdateUserNotification(UserNotification userNotification) => Update(userNotification);

        public void CreateUserNotification(UserNotification userNotification) => Create(userNotification);
    }
}