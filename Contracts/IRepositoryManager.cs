using System.Threading.Tasks;

namespace Contracts
{
    public interface IRepositoryManager
    {
        IForumRepository Forum { get; }
        ITopicRepository Topic { get; }
        IMessageRepository Message { get; }
        Task SaveAsync();
    }
}