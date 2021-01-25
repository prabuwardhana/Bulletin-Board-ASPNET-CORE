using System.Collections.Generic;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace Entities.ViewModels
{
    public class RoleEditViewModels 
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<User> Members { get; set; }
        public IEnumerable<User> NonMembers { get; set; }
    }
}