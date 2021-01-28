using System.Collections.Generic;
using BulletinBoard.Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace BulletinBoard.Entities.ViewModels
{
    public class RoleEditViewModels 
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}