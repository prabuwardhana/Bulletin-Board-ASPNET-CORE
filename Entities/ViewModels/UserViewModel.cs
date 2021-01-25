using System;
using System.Collections.Generic;

namespace Entities.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public DateTime RegisterDateTime { get; set; }
        public DateTime LastLoginDateTime { get; set; }
        public bool IsAdministrator { get; set; }
        public bool IsLocked { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}