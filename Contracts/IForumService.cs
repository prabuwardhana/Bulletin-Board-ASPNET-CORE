using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Entities.ViewModels;

namespace Contracts
{
    public interface IForumService
    {
        Task<(PagedList<ForumListViewModel>, IEnumerable<ForumListViewModel>)> GetPagedAndTopForumsAsync(ForumParameters forumParameters);
        Task CreateForumAsync(ForumForCreationViewModel model, string userName);
        Task<ForumViewModel> GetForumDetailAsync(int id);
        Task<ForumForUpdateViewModel> GetForumDetailForEditingAsync(int id);
        Task EditForumAsync(ForumForUpdateViewModel model);
        Task DeleteForumAsync(ForumViewModel model);
    }
}