using System;

namespace BulletinBoard.Entities.ViewModels
{
    public class MessageViewModel
    {
        public string Id { get; set; }
        public string FromUserId { get; set; }
        public string FromUserImageUrl { get; set; }
        public string FromUserName { get; set; }
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public DateTime SendDateTime { get; set; }
        public bool IsRead { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}