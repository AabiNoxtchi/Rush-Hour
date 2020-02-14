using DataAccess.Entity;
using DataAccess.Service;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Users
{
    public class EditVM : BaseEditVM<User>
    {
        [DisplayName("Name:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Name { get; set; }

        public string Phone { get; set; }
      
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Password:")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match")]
        public string ConfirmPassword { get; set; }

        public bool IsAdmin { get; set; }

        public System.Guid ActivationCode { get; set; }

        public bool IsEmailVerified { get; set; }

        public EditVM() { }

        public override void PopulateEntity(User item)
        {       

            item.Id = Id;          
            item.Name = Name;
            item.Phone = Phone;
            item.Email = Email;
            item.Password = Password;
            item.IsAdmin = IsAdmin;
            item.IsEmailVerified = IsEmailVerified;
            item.ActivationCode = ActivationCode;
        }

        public override void PopulateModel(User item)
        {
            Id = item.Id;           
            Name = item.Name;
            Phone = item.Phone;
            Email = item.Email;
            Password = item.Password;
            IsAdmin = item.IsAdmin;
            IsEmailVerified = item.IsEmailVerified;
            ActivationCode = item.ActivationCode;
        }
    }
}