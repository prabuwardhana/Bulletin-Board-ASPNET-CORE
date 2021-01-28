using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Entities.ViewModels
{
    public class UserForLogInViewModel
    {
        [Required, DisplayName("E-Mail Address")]
        public string Email { get; set; }

        [Required, DisplayName("Password")]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}