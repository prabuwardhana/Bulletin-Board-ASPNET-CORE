using System;
using System.Collections.Generic;
using Entities.Models;

namespace Entities.ViewModels
{
    public class TopicViewModel
    {
        public int id { get; set; }
        public int ForumId { get; set; }
        public string OwnerUserName { get; set; }
        public User Owner { get; set; }
        public IEnumerable<string> OwnerRoles { get; set; }
        public DateTime PostDateTime { get; set; }
        public string ModifiedByUser { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public bool IsLocked { get; set; }
        public int? RootTopicId { get; set; }
        public ICollection<TopicViewModel> InverseReplyToTopic { get; set; }
    }
}