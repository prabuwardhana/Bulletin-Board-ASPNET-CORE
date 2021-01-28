using System;
using System.Linq;
using BulletinBoard.Entities.Models;
using System.Linq.Dynamic.Core;
using BulletinBoard.Repository.Extensions.Utility;

namespace BulletinBoard.Repository.Extensions
{
    public static class RepositoryForumExtensions
    {
        public static IQueryable<Forum> Search(this IQueryable<Forum> forums, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return forums;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return forums.Where(e => e.Name.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Forum> Sort(this IQueryable<Forum> forums, string orderByQueryString)
        {
            if (String.IsNullOrWhiteSpace(orderByQueryString))
                return forums.OrderByDescending(f => f.CreatedDateTime);
            
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Forum>(orderByQueryString);

            if (String.IsNullOrWhiteSpace(orderQuery))
                return forums.OrderByDescending(f => f.CreatedDateTime);
            
            return forums.OrderBy(orderQuery);
        }
    }
}