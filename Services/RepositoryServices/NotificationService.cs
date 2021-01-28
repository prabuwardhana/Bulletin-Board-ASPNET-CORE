using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Infrastructure.SignalR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace Services.RepositoryServices
{
    public class NotificationService : INotificationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;
        private IHubContext<SignalServer> _hubContext;

        public NotificationService(IRepositoryManager repositoryManager,
                                   UserManager<User> userManager,
                                   IHubContext<SignalServer> hubContext)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _hubContext = hubContext;
        }        

        public async Task NotifyMsgReceiverAsync(Notification notification, string userId)
        {
            _repositoryManager.Notification.CreateNotification(notification);
            await _repositoryManager.SaveAsync();

            var userNotification = new UserNotification() {
                UserId = userId,
                NotificationId = notification.Id
            };

            _repositoryManager.UserNotification.CreateUserNotification(userNotification);
            await _repositoryManager.SaveAsync();

            await _hubContext.Clients.All.SendAsync("displayNotification","");
        }

        public async Task<IEnumerable<UserNotification>> GetAllAssignedNotificationAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var userNotification = await _repositoryManager.UserNotification.GetUserNotificationsAsync(user.Id, trackChanges: false);
            
            return userNotification;
        }

        public async Task ReadNotificationAsync(int notificationId, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var notification = await _repositoryManager.UserNotification.GetUserNotificationAsync(notificationId, user.Id, trackChanges: false);

            notification.IsRead = true;

            _repositoryManager.UserNotification.UpdateUserNotification(notification);
            await _repositoryManager.SaveAsync();
        }
    }
}