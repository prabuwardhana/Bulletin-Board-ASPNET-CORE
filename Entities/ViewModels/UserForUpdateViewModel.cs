using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Entities.ViewModels
{
    public class UserForUpdateViewModel : UserForManipulationViewModel
    {
        [DisplayName("ID")]
        public string Id { get; set; }        
        [DisplayName("Profile Picture")]
        public string ImageUrl { get; set; }
        [DisplayName("Current Password")]
        public string CurrentPassword { get; set; }
        [DisplayName("New Password")]
        public string Password { get; set; }
        [DisplayName("New Password Again")]
        public string RepeatPassword { get; set; }
        [DisplayName("Administrator")]
        public bool IsAdministrator { get; set; }
        [DisplayName("Lock")]
        public bool IsLocked { get; set; }
        [DisplayName("Register Date")]
        public DateTime RegisterDateTime { get; set; }
        [DisplayName("Last Log In Date")]
        public DateTime LastLogInDateTime { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
