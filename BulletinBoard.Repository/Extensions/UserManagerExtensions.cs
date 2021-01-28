using System;
using System.Linq;
using BulletinBoard.Entities.Models;
using System.Linq.Dynamic.Core;
using BulletinBoard.Repository.Extensions.Utility;

namespace BulletinBoard.Repository.Extensions
{
    public static class UserManagerExtensions
    {
        public static IQueryable<User> Search(this IQueryable<User> users, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return users;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return users.Where(e => e.UserName.ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<User> Sort(this IQueryable<User> users, string orderByQueryString)
        {
            if (String.IsNullOrWhiteSpace(orderByQueryString))
                return users.OrderByDescending(u => u.RegisterDateTime);
            
            var orderQuery = OrderQueryBuilder.CreateOrderQuery<User>(orderByQueryString);

            if (String.IsNullOrWhiteSpace(orderQuery))
                return users.OrderByDescending(u => u.RegisterDateTime);
            
            return users.OrderBy(orderQuery);
        }
    }
}