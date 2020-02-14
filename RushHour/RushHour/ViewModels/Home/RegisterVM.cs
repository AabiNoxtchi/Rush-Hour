using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Home
{
    public class RegisterVM
    {
        [Required]
        public string Name { get; set; }

         public string Phone { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        //[DataType(DataType.EmailAddress,ErrorMessage ="Valid Email Address Required")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name="Confirm Password")]
        [Compare("Password",ErrorMessage ="Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        public bool IsEmailVerified { get; set; }
        public StatusVM statusVM { get; set; }



        public RegisterVM() { }

        public void PopulateEntity(User item)
        {
            
            item.Email = Email;
            item.Password = Password;           
            item.Name = Name;
            item.Phone = Phone;
            item.IsEmailVerified = IsEmailVerified;
        }

       
    }
}