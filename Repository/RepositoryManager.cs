using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private IForumRepository _forumRepository;
        private ITopicRepository _topicRepository;
        private IMessageRepository _messageRepository;
        private INotificationRepository _notifcationRepository;
        private IUserNotificationRepository _userNotificationRepository;

        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public IForumRepository Forum
        {
            get
            {
                if (_forumRepository == null)
                    _forumRepository = new ForumRepository(_repositoryContext);
                
                return _forumRepository;
            }
        }

        public ITopicRepository Topic
        {
            get
            {
                if (_topicRepository == null)
                    _topicRepository = new TopicRepository(_repositoryContext);
                
                return _topicRepository;
            }
        }

        public IMessageRepository Message
        {
            get
            {
                if (_messageRepository == null)
                    _messageRepository = new MessageRepository(_repositoryContext);
                
                return _messageRepository;
            }
        }

        public INotificationRepository Notification
        {
            get
            {
                if (_notifcationRepository == null)
                    _notifcationRepository = new NotificationRepository(_repositoryContext);
                
                return _notifcationRepository;
            }
        }

        public IUserNotificationRepository UserNotification
        {
            get
            {
                if (_userNotificationRepository == null)
                    _userNotificationRepository = new UserNotificationRepository(_repositoryContext);
                
                return _userNotificationRepository;
            }
        }

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}