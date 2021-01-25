using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// add comment
namespace BulletinBoard.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IBlobService _blobService;

        public UserController(IUserService userService, IBlobService blobService)
        {
            _userService = userService;
            _blobService = blobService;
        }

        [HttpGet]
        public async Task<IActionResult> Detail(string username)
        {
            var model = await _userService.GetUserViewModelAsync(username);

            return View(model);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string username)
        {
            if (User.Identity.Name != username && !User.IsInRole(Roles.Administrator))
            {
                throw new Exception("Operation is denied.");
            }

            var model = await _userService.GetUserForUpdateViewModelAsync(username);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserForUpdateViewModel model, IFormFile file)
        {
            var user = await _userService.GetUserAsync(model.Id);

            if (user != null)
            {
                // Email validation
                var emailValidationResult = await _userService.ValidateEmailAsync(user);

                if (!emailValidationResult.Succeeded)
                {
                    AddErrorsFromResult(emailValidationResult);
                }

                // Password validation
                var passwordValidationResult = await _userService.ValidatePasswordAsync(user, model);

                if (passwordValidationResult == null || !passwordValidationResult.Succeeded)
                {
                    AddErrorsFromResult(emailValidationResult);
                }              

                // Azure blob storage
                if (file != null)
                {
                    // Remove old profile image if any
                    if (user.ImageUrl != null)
                    {
                        var fullUri = new Uri(user.ImageUrl);
                        var fileName = fullUri.Segments.Last();
                        await _blobService.DeleteBlobAsync(fileName);
                    }

                    // Upload new profile image
                    user.ImageUrl = await _blobService.UploadContentBlobAsync(file, ModelState);
                }

                // Update User entity
                if ((emailValidationResult.Succeeded && passwordValidationResult == null)
                    || (emailValidationResult.Succeeded && passwordValidationResult.Succeeded))
                {
                    var result = await _userService.UpdateUserAsync(model, user);
                    
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Detail", new { userName = user.UserName });
                    }
                    else
                    {
                        foreach (IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "User Not Found");
            }            

            return View(user);
        }

        [HttpGet, Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index(UserParameters userParameters)
        {
            var models = await _userService.GetUserViewModelsAsync(userParameters);

            return View(models);
        }
    }
}