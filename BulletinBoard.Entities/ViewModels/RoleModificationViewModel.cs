using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Entities.ViewModels
{
     public class RoleModificationViewModel
     {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}