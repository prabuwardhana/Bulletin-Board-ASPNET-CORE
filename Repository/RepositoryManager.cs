using System.Threading.Tasks;
using Contracts;
using Entities;
using Repository;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;

        private IForumRepository _forumRepository;
        private ITopicRepository _topicRepository;
        private IMessageRepository _messageRepository;

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

        public Task SaveAsync() => _repositoryContext.SaveChangesAsync();
    }
}