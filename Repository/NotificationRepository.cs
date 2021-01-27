using Contracts;
using Entities;
using Entities.Models;

namespace Repository
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(RepositoryContext repositorycontext) : base(repositorycontext)
        {

        }        

        public void CreateNotification(Notification notification) => Create(notification);
    }
}