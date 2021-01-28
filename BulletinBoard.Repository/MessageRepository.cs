using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Contracts;
using BulletinBoard.Entities;
using BulletinBoard.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BulletinBoard.Repository
{
    public class MessageRepository : RepositoryBase<Message>, IMessageRepository
    {
        public MessageRepository(RepositoryContext repositorycontext) : base(repositorycontext)
        {
            
        }

        public async Task<IEnumerable<Message>> GetMessagesForUserAsync(string userName, bool trackChanges = false) =>
            await FindByCondition(m => m.ToUser.UserName == userName || m.FromUser.UserName == userName, trackChanges)
            .Include("ToUser")
            .Include("FromUser")
            .ToListAsync();
        
        public async Task<Message> GetMessageForUserAsync(int messageId, bool trackChanges = false) =>
            await FindByCondition(m => m.id.Equals(messageId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<Message> GetMessageDetailForUserAsync(int messageId, bool trackChanges = false) =>
            await FindByCondition(m => m.id.Equals(messageId), trackChanges)
            .Include("ToUser")
            .Include("FromUser")
            .SingleOrDefaultAsync();
        
        public async Task<Message> GetMessageToReplyAsync(int messageId, bool trackChanges = false) =>
            await FindByCondition(m => m.id.Equals(messageId), trackChanges)
            .Include(m => m.FromUser)
            .SingleOrDefaultAsync();

        public void CreateMessage(Message message) => Create(message);

        public void UpdateMessage(Message message) => Update(message);

        public void DeleteMessage(Message message) => Delete(message);        
    }
}