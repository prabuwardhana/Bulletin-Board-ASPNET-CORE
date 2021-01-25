using System;

namespace Entities.ViewModels
{
    public class TopicForUpdateViewModel : TopicForManipulationViewModel
    {        
        public string ModifiedByUserId { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}