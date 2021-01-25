using System;

namespace Entities.ViewModels
{
    public class MessageReplyViewModel
    {
        public string ReplyToUserId { get; set; }
        public string ReplyToUserName { get; set; }
        public string Title { get; set; }        
        public string Content { get; set; } 
        public DateTime SendDateTime { get; set; }
    }
}