using System;
using System.Threading.Tasks;
using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
{
    public class TopicController : Controller
    {
        private readonly ITopicService _topicService;

        public TopicController(ITopicService serviceManager)
        {
            _topicService = serviceManager;
        }

        public async Task<IActionResult> Index(int forumId)
        {
            var model = await _topicService.GetPagedAndTopTopicsFromForumAsync(forumId);

            return View(model);
        }

        [HttpGet]
        public IActionResult Create(int forumId)
        {            
            var model = new TopicForCreationViewModel { ForumId = forumId};
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TopicForCreationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information.");
            }

            await _topicService.CreateTopicAsync(model, User.Identity.Name);

            return RedirectToAction("Index", new { forumId = model.ForumId });
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _topicService.GetTopicWithAllRepliesAsync(id);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Reply(int toId)
        {
            var model = await _topicService.GetTopicForReplyingAsync(toId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(TopicForReplyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information.");
            }

            await _topicService.ReplyToTopicAsync(model, User.Identity.Name);

            return RedirectToAction("Detail", new { id = model.RootTopicId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = await _topicService.GetTopicDetailAsync<TopicForUpdateViewModel>(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(TopicForUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information.");
            }

            await _topicService.EditTopicAsync(model, User.Identity.Name);

            return RedirectToAction("Detail", new { id = model.RootTopicId });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _topicService.GetTopicDetailAsync<TopicViewModel>(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(TopicViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid topic information.");
            }

            await _topicService.DeleteTopicAsync(model);

            if (model.id == model.RootTopicId)
            {
                return RedirectToAction("Index", new { forumId = model.ForumId });
            }

            return RedirectToAction("Detail", new { id = model.RootTopicId });
        }
    }
}