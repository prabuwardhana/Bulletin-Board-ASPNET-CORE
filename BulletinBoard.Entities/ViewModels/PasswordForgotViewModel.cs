using System.ComponentModel.DataAnnotations;

namespace BulletinBoard.Entities.ViewModels
{
    public class PasswordForgotViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}