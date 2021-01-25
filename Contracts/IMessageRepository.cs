using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IMessageRepository
    {
        Task<IEnumerable<Message>> GetMessagesForUserAsync(string userId, bool trackChanges);
        Task<Message> GetMessageForUserAsync(int messageId, bool trackChanges);
        Task<Message> GetMessageDetailForUserAsync(int messageId, bool trackChanges);
        Task<Message> GetMessageToReplyAsync(int messageId, bool trackChanges);
        void CreateMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(Message message);
    }
}