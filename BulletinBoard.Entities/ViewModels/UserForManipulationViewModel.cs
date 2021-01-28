using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Entities.ViewModels
{
    public class UserForManipulationViewModel
    {
        [Required, DisplayName("Name")]
        public string Name { get; set; }
        [Required, DisplayName("Username")]
        public string Username { get; set; }
        [Required, DisplayName("Email")]
        public string Email { get; set; }
        [Required, DisplayName("Self-Introduction")]
        public string Description { get; set; }
    }
}