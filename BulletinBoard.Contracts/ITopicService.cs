using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Entities.ViewModels;

namespace BulletinBoard.Contracts
{
    public interface ITopicService
    {
        Task<(ForumViewModel, IEnumerable<TopicListViewModel>)> GetPagedAndTopTopicsFromForumAsync(int forumId);
        Task CreateTopicAsync(TopicForCreationViewModel model, string userName);
        Task<T> GetTopicDetailAsync<T>(int id);
        Task<TopicViewModel> GetTopicWithAllRepliesAsync(int id);
        Task<TopicForReplyViewModel> GetTopicForReplyingAsync(int toId);
        Task ReplyToTopicAsync(TopicForReplyViewModel model, string userName);
        Task EditTopicAsync(TopicForUpdateViewModel model, string userName);
        Task DeleteTopicAsync(TopicViewModel model);
    }
}