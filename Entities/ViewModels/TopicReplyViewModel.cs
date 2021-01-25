using Entities.Models;

namespace Entities.ViewModels
{
    public class TopicForReplyViewModel : TopicForCreationViewModel
    {
        public int? ReplyToTopicId { get; set; }        
        public Topic ReplyToTopic { get; set; }
    }
}