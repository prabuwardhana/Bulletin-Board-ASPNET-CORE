using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;

namespace Services.IdentityUserService
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IUserValidator<User> _userValidator;
        private readonly IPasswordValidator<User> _passwordValidator;
        private readonly IPasswordHasher<User> _passwordHasher;
        
        public UserService(IMapper mapper,
                           UserManager<User> userMgr,
                           IUserValidator<User> userValidator,
                           IPasswordValidator<User> passValidator,
                           IPasswordHasher<User> passwordHash)
        {
            _mapper = mapper;
            _userManager = userMgr;
            _userValidator = userValidator;
            _passwordValidator = passValidator;
            _passwordHasher = passwordHash;
        }

        public async Task<User> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public async Task<UserForUpdateViewModel> GetUserForUpdateViewModelAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            
            if (user == null)
            {
                throw new Exception($"User '{username}' does not exist.");
            }

            var model = _mapper.Map<UserForUpdateViewModel>(user);
            model.Roles = await _userManager.GetRolesAsync(user);

            return model;
        }

        public async Task<UserViewModel> GetUserViewModelAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                throw new Exception($"User '{username}' does not exist.");
            }

            var model = _mapper.Map<UserViewModel>(user);
            model.Roles = await _userManager.GetRolesAsync(user);

            return model;
        }

        public async Task<PagedList<UserViewModel>> GetUserViewModelsAsync(UserParameters userParameters)
        {
            var users = await _userManager.Users
                                        .Search(userParameters.SearchTerm)
                                        .Sort(userParameters.OrderBy)
                                        .ToListAsync();
            var pagedUser = PagedList<User>.ToPagedList(users, userParameters.PageNumber, userParameters.PageSize);

            var pagedUserModel = _mapper.Map<PagedList<UserViewModel>>(pagedUser);

            pagedUserModel.MetaData = pagedUser.MetaData;

            return pagedUserModel;
        }

        public async Task<IdentityResult> ValidateEmailAsync(User user)
        {
            IdentityResult validationResult = await _userValidator.ValidateAsync(_userManager, user);

            return validationResult;
        }

        public async Task<IdentityResult> ValidatePasswordAsync(User user, UserForUpdateViewModel model)
        {
            IdentityResult validationResult = null;

            if (!string.IsNullOrEmpty(model.Password))
            {
                model.Password = model.Password.Trim();
                model.RepeatPassword = model.RepeatPassword.Trim();
                if (!model.Password.Equals(model.RepeatPassword))
                {
                    throw new Exception("Passwords are not identical.");
                }

                validationResult = await _passwordValidator.ValidateAsync(_userManager, user, model.Password);
            }

            return validationResult;
        }

        public async Task<IdentityResult> UpdateUserAsync(UserForUpdateViewModel model, User user)
        {
            user.Email = model.Email;
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
            user.IsAdministrator = model.IsAdministrator;
            user.IsLocked = model.IsLocked;
            user.Description = model.Description;
            
            IdentityResult result = await _userManager.UpdateAsync(user);
            
            return result;
        }
    }
}