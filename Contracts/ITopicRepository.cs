using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface ITopicRepository
    {
        Task<ICollection<Topic>> GetTopicsForForumWithReplyAsync(int forumId, bool trackChanges);
        Task<Topic> GetTopicByIdAsync(int topicId, bool trackChanges);
        Task<Topic> GetTopicDetailByIdAsync(int topicId, bool trackChanges);
        Task<ICollection<Topic>> GetInverseReplyToTopicAsync(int topicId, bool trackChanges);
        Task<ICollection<Topic>> GetDescendantTopicsAsync(int topicId, bool trackChanges);
        Task<ICollection<Topic>> GetTopTopicsAsync(bool trackChanges);
        void CreateTopicForForum(Topic topic);
        void DeleteTopic(Topic topic);
        void UpdateTopic(Topic topic);
        void CascadeDeleteTopic(ICollection<Topic> topic);
    }    
}