using System;

namespace BulletinBoard.Entities.ViewModels
{
    public class TopicForUpdateViewModel : TopicForManipulationViewModel
    {        
        public string ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}