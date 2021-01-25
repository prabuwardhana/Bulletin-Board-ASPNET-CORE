using System;

namespace Entities.ViewModels
{
    public class TopicForCreationViewModel : TopicForManipulationViewModel
    {
        public string OwnerId { get; set; }
        public DateTime PostDateTime { get; set; }
    }
}