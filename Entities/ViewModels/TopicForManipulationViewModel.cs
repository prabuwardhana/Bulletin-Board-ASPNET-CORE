using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class TopicForManipulationViewModel
    {
        public int id { get; set; }        
        public int ForumId { get; set; }
        public int? RootTopicId { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }
        public bool IsLocked { get; set; }
    }
}