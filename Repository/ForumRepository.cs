using System.Linq;
using Entities;
using Entities.Models;
using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.RequestFeatures;
using Repository.Extensions;

namespace Repository
{
    public class ForumRepository : RepositoryBase<Forum>, IForumRepository
    {
        public ForumRepository(RepositoryContext repositorycontext) : base(repositorycontext)
        {
        }

        public async Task<PagedList<Forum>> GetForumsAsync(ForumParameters forumParameters, bool trackChanges)
        {
            var forums = await FindAll(trackChanges)
                    .Search(forumParameters.SearchTerm)
                    .Sort(forumParameters.OrderBy)
                    .Include(f => f.Owner)
                    .Include(f => f.Topic)
                    .ToListAsync();

            return PagedList<Forum>.ToPagedList(forums, forumParameters.PageNumber, forumParameters.PageSize);
        }

        public async Task<Forum> GetForumByIdAsync(int forumId, bool trackChanges) => 
            await FindByCondition(f => f.id.Equals(forumId), trackChanges)
            .SingleOrDefaultAsync();
        
        public async Task<Forum> GetForumDetailByIdAsync(int forumId, bool trackChanges) => 
            await FindByCondition(f => f.id.Equals(forumId), trackChanges)
            .Include(f => f.Owner)
            .SingleOrDefaultAsync();
        
        public async Task<IEnumerable<Forum>> GetTopForumsAsync(bool trackChanges) =>
            await FindByCondition(f => f.Topic.Count > 0, trackChanges)
                .Include(f => f.Topic)
                .OrderByDescending(f => f.Topic.Count(t => t.ReplyToTopicId == null))
                .Take(10)
                .ToListAsync();

        public void CreateForum(Forum forum) => Create(forum);

        public void UpdateForum(Forum forum) => Update(forum);

        public void DeleteForum(Forum forum) => Delete(forum);        
    }
}