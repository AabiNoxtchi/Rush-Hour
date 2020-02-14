using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Home
{
    public class LoginVM
    {
        [DisplayName("Email:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Email { get; set; }
        [DisplayName("Password:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }
    }
}