using DataAccess.Entity;
using RushHour.Attributes.FilterVM;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace RushHour.ViewModels.Users
{
    public class FilterVM : BaseFilterVM<User>
    {
        
        [DisplayName("Email:")]
        public string Email { get; set; }
        [DisplayName("Name:")]
        public string Name { get; set; }
       

        public override Expression<Func<User, bool>> GenerateFilter()
        {
            return i => (string.IsNullOrEmpty(Email) || i.Email.Contains(Email)) &&
                        (string.IsNullOrEmpty(Name) || i.Name.Contains(Name)
                       );
        }
    }
}