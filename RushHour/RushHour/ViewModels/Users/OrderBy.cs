using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Users
{
    public class OrderBy:BaseOrderBy<User>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public override Func<IQueryable<User>, IOrderedQueryable<User>> orderBy()
        {
            if (!string.IsNullOrEmpty(Email))
            {
                return u => u.OrderBy(i => i.Email);
            }
            else if (!string.IsNullOrEmpty(Name))
                {
                return u => u.OrderBy(i => i.Name);
            }
            return null;
        }
    }
}