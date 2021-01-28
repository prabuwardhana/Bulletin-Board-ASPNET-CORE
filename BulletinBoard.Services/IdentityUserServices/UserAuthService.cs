using System;
using System.Threading.Tasks;
using AutoMapper;
using BulletinBoard.Contracts;
using BulletinBoard.Entities.Models;
using BulletinBoard.Entities.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace BulletinBoard.Services.IdentityUserService
{
    public class UserAuthService : IUserAuthService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        
        public UserAuthService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterNewUserAsync(UserForRegistrationViewModel model)
        {
            var user = _mapper.Map<User>(model);

            // Create a new user
            IdentityResult regResult = await _userManager.CreateAsync(user, model.Password);

            if (regResult.Succeeded)
            {
                // Sign the user in when registration is succeeded
                var signInResult = await _signInManager.PasswordSignInAsync(user,
                    model.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);
                
                if (signInResult.Succeeded)
                {
                    user.RegisterDateTime = DateTime.Now;
                    user.LastLoginDateTime = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                }
            }

            return regResult;
        }

        public async Task<bool> SignInUserAsync(UserForLogInViewModel model, string returnUrl)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                await _signInManager.SignOutAsync();

                var result = await _signInManager.PasswordSignInAsync(user,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    user.LastLoginDateTime = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                    return true;
                }
            }

            return false;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<string> GetPasswordResetTokenAsync(PasswordForgotViewModel model)
        {
            var token = "";

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
                token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return token;
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(PasswordResetViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                IdentityResult resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                return resetPassResult;
            }
            
            return null;
        }
    }
}