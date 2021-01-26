using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
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

        public IActionResult ReadNotification(int notificationId)
        {
            _notificationService.ReadNotification(notificationId, User.Identity.Name);

            return Ok();
        }
    }
}

