using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Entities.ViewModels 
{
    public class UserForRegistrationViewModel : UserForManipulationViewModel
    {
        [Required, DisplayName("Password")]
        public string Password { get; set; }
        [Required, DisplayName("Repeat Password")]
        public string RepeatPassword { get; set; }
    }
}