using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.RequestFeatures;
using Entities.ViewModels;

namespace Contracts
{
    public interface IForumService
    {
        Task<(PagedList<ForumListViewModel>, IEnumerable<ForumListViewModel>)> GetForumsList(ForumParameters forumParameters);
        Task CreateForum(ForumForCreationViewModel model, string userName);
        Task<ForumViewModel> GetForumDetail(int id);
        Task<ForumForUpdateViewModel> GetForumDetailForEditing(int id);
        Task EditForum(ForumForUpdateViewModel model);
        Task DeleteForum(ForumViewModel model);
    }
}