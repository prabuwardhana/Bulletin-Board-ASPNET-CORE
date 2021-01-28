using BulletinBoard.Contracts;
using BulletinBoard.Entities;
using BulletinBoard.Entities.Models;

namespace BulletinBoard.Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext repositorycontext) : base(repositorycontext)
        {

        }        

        public void CreateNotification(Notification notification) => Create(notification);
    }
}