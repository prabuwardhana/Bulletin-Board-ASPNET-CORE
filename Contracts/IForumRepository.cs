using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;

namespace Contracts
{
    public interface IForumRepository
    {
        Task<PagedList<Forum>> GetForumsAsync(ForumParameters forumParameters, bool trackChanges);
        Task<Forum> GetForumByIdAsync(int forumId, bool trackChanges);
        Task<Forum> GetForumDetailByIdAsync(int forumId, bool trackChanges);
        Task<IEnumerable<Forum>> GetTopForumsAsync(bool trackChanges);
        void CreateForum(Forum forum);
        void UpdateForum(Forum forum);
        void DeleteForum(Forum forum);
    }
}
