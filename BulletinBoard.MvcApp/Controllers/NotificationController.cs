using System.Linq;
using System.Threading.Tasks;
using BulletinBoard.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.MvcApp.Controllers
{
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<IActionResult> GetNotification()
        {
            var userNotification = await _notificationService.GetAllAssignedNotificationAsync(User.Identity.Name);
            return Ok(new { UserNotification = userNotification, Count = userNotification.Count() });
        }

        public async Task<IActionResult> ReadNotification(int notificationId)
        {
            await _notificationService.ReadNotificationAsync(notificationId, User.Identity.Name);

            return Ok();
        }
    }
}

