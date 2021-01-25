using System.Linq;
using Entities.Models;
using Contracts;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public class TopicRepository : RepositoryBase<Topic>, ITopicRepository
    {
        public TopicRepository(RepositoryContext repositorycontext) : base(repositorycontext)
        {
        }

        public async Task<ICollection<Topic>> GetTopicsForForumWithReplyAsync(int forumId, bool trackChanges) => 
            await FindByCondition(t => t.ForumId.Equals(forumId) && t.ReplyToTopicId == null, trackChanges)
            .Include(t => t.Owner)
            .Include(t => t.InverseRootTopic)
            .ToListAsync();

        public async Task<Topic> GetTopicByIdAsync(int topicId, bool trackChanges) => 
            await FindByCondition(t => t.id.Equals(topicId), trackChanges)
            .SingleOrDefaultAsync();
        
        public async Task<Topic> GetTopicDetailByIdAsync(int topicId, bool trackChanges) => 
            await FindByCondition(t => t.id.Equals(topicId), trackChanges)
            .Include(t => t.Owner)
            .Include(t => t.ModifiedByUser)
            .SingleOrDefaultAsync();

        public async Task<ICollection<Topic>> GetInverseReplyToTopicAsync(int topicId, bool trackChanges) => 
            await FindByCondition(t => t.RootTopicId.Equals(topicId) && t.ReplyToTopicId != null, trackChanges)
            .Include(t => t.Owner)
            .Include(t => t.ModifiedByUser)
            .ToListAsync();
        
        public async Task<ICollection<Topic>> GetDescendantTopicsAsync(int topicId, bool trackChanges) =>
            await FindByCondition(t => t.RootTopicId == topicId, trackChanges)
            .ToListAsync();
        
        public async Task<ICollection<Topic>> GetTopTopicsAsync(bool trackChanges) =>
            await FindByCondition(t => t.InverseRootTopic.Count > 1, trackChanges)
                .Include(t => t.InverseRootTopic)
                .OrderByDescending(t => t.InverseRootTopic.Count)
                .Take(5)
                .ToListAsync();

        public void CreateTopicForForum(Topic topic) => Create(topic);

        public void UpdateTopic(Topic topic) => Update(topic);

        public void DeleteTopic(Topic topic) => Delete(topic);

        public void CascadeDeleteTopic(ICollection<Topic> topic) => CascadeDelete(topic);
    }
}