using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Contracts
{
    public interface IUserService
    {
        Task<User> GetUserAsync(string userId);
        Task<UserForUpdateViewModel> GetUserForUpdateViewModelAsync(string username);
        Task<UserViewModel> GetUserViewModelAsync(string username);
        Task<PagedList<UserViewModel>> GetUserViewModelsAsync(UserParameters userParameters);
        Task<IdentityResult> ValidateEmailAsync(User user);
        Task<IdentityResult> ValidatePasswordAsync(User user, UserForUpdateViewModel model);
        Task<IdentityResult> UpdateUserAsync(UserForUpdateViewModel model, User user);        
    }
}