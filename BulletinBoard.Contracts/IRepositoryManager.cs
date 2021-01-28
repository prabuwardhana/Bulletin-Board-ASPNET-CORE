using System.Threading.Tasks;

namespace BulletinBoard.Contracts
{
    public interface IRepositoryManager
    {
        IForumRepository Forum { get; }
        ITopicRepository Topic { get; }
        IMessageRepository Message { get; }
        INotificationRepository Notification { get; }
        IUserNotificationRepository UserNotification { get; }
        Task SaveAsync();
    }
}