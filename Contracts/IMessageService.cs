using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.ViewModels;

namespace Contracts
{
    public interface IMessageService
    {
        Task<(IEnumerable<MessageViewModel>, string)> GetAllCurrentUserMessagesAsync(string userName);
        Task<MessageViewModel> GetBlankMessageAsync(string toUserName);
        Task SendDirectMessageAsync(MessageViewModel model, string userName);
        Task<MessageViewModel> ReadMessageAsync(int msgId, string userName);
        Task<MessageReplyViewModel> GetMessageToReplyAsync(int msgId);
        Task SendMessageReplyAsync(MessageReplyViewModel model, string userName);
        Task DeleteMessageAsync(int msgId, string userName);
    }
}