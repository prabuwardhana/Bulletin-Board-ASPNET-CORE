using BulletinBoard.Entities.Models;

namespace BulletinBoard.Entities.ViewModels
{
    public class TopicForReplyViewModel : TopicForCreationViewModel
    {
        public int? ReplyToTopicId { get; set; }        
        public Topic ReplyToTopic { get; set; }
    }
}