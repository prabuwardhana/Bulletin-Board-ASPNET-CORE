using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class ForumForManipulationViewModel
    {        
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
        [DisplayName("Forum Image")]
        public string ForumImageUri { get; set; }
        public string OwnerId { get; set; }
        public string OwnerUserName { get; set; }
        public DateTime CreatedDateTime { get; set; }        
    }
}