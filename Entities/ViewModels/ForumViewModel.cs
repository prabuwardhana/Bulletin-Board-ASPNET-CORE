using System;
using System.Collections.Generic;
using Entities.Models;

namespace Entities.ViewModels
{
    public class ForumViewModel
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string ForumImageUri { get; set; }
        public string OwnerUserName { get; set; }
        public string Description { get; set; }        
        public bool IsLocked { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public ICollection<Topic> Topic { get; set; }
    }
}