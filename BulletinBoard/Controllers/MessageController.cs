using System;
using System.Threading.Tasks;
using Contracts;
using Entities.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.Controllers
{
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService serviceManager)
        {
            _messageService = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var messageUser = await _messageService.GetMessagesListAsync(User.Identity.Name);

            return View(messageUser);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string toUserName)
        {
            var model = await _messageService.GetBlankMessageAsync(toUserName);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MessageViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid message information.");
            }

            await _messageService.SendDirectMessageAsync(model, User.Identity.Name);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var model = await _messageService.ReadMessageAsync(id, User.Identity.Name);

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Reply(int id)
        {
            var model = await _messageService.GetMessageToReplyAsync(id);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Reply(MessageReplyViewModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new Exception("Invalid message information.");
            }

            await _messageService.SendMessageReply(model, User.Identity.Name);
            
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteMessage(id, User.Identity.Name);

            return RedirectToAction("Index");
        }
    }
}