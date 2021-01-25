using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.ViewModels;

namespace Contracts
{
    public interface ITopicService
    {
        Task<(ForumViewModel, IEnumerable<TopicListViewModel>)> GetTopicsListFromForum(int forumId);
        Task CreateTopic(TopicForCreationViewModel model, string userName);
        Task<T> GetTopicDetail<T>(int id);
        Task<TopicViewModel> GetTopicWithAllReplies(int id);
        Task<TopicForReplyViewModel> GetTopicForReplying(int toId);
        Task ReplyToTopic(TopicForReplyViewModel model, string userName);
        Task EditTopic(TopicForUpdateViewModel model, string userName);
        Task DeleteTopic(TopicViewModel model);
    }
}