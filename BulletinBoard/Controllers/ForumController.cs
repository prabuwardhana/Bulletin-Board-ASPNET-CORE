using System;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
{
    public class ForumController : Controller
    {
        private readonly IForumService _forumService;
        private readonly IBlobService _blobService;

        public ForumController(IForumService serviceManager, IBlobService blobService)
        {
            _forumService = serviceManager;
            _blobService = blobService;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index(ForumParameters forumParameters)
        {
            var model = await _forumService.GetPagedAndTopForumsAsync(forumParameters);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        public async Task<IActionResult> Create(ForumForCreationViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid forum information.");
            }

            model.ForumImageUri = await _blobService.UploadContentBlobAsync(file, ModelState);

            await _forumService.CreateForumAsync(model, User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _forumService.GetForumDetailAsync(id);
            
            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _forumService.GetForumDetailForEditingAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        public async Task<IActionResult> Edit(ForumForUpdateViewModel model, IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid forum information.");
            }

            // Azure blob storage
            if (file != null)
            {
                // Remove old forum image if any
                if (model.ForumImageUri != null)
                {
                    var fullUri = new Uri(model.ForumImageUri);
                    var fileName = fullUri.Segments.Last();
                    await _blobService.DeleteBlobAsync(fileName);
                }

                // Upload new profile image
                model.ForumImageUri = await _blobService.UploadContentBlobAsync(file, ModelState);
            }

            await _forumService.EditForumAsync(model);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _forumService.GetForumDetailAsync(id);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = Roles.Administrator + "," + Roles.Moderator)]
        public async Task<IActionResult> Delete(ForumViewModel model)
        {
            // Remove associated Forum image if any
            if (model.ForumImageUri != null)
            {
                var fullUri = new Uri(model.ForumImageUri);
                var fileName = fullUri.Segments.Last();
                await _blobService.DeleteBlobAsync(fileName);
            }

            await _forumService.DeleteForumAsync(model);

            return RedirectToAction("Index");
        }
    }
}