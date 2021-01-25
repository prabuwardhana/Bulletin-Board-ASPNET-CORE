using System.ComponentModel.DataAnnotations;

namespace Entities.ViewModels
{
    public class PasswordForgotViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}