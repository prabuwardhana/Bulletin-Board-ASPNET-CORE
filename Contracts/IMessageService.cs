using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.ViewModels;

namespace Contracts
{
    public interface IMessageService
    {
        Task<(IEnumerable<MessageViewModel>, string)> GetMessagesListAsync(string userName);
        Task<MessageViewModel> GetBlankMessageAsync(string toUserName);
        Task SendDirectMessageAsync(MessageViewModel model, string userName);
        Task<MessageViewModel> ReadMessageAsync(int msgId, string userName);
        Task<MessageReplyViewModel> GetMessageToReplyAsync(int msgId);
        Task SendMessageReply(MessageReplyViewModel model, string userName);
        Task DeleteMessage(int msgId, string userName);
    }
}