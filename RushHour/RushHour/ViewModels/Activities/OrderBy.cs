using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Activities
{
    public class OrderBy:BaseOrderBy<Activity>
    {
        public string Name { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public override Func<IQueryable<Activity>, IOrderedQueryable<Activity>> orderBy()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                return u => u.OrderBy(i => i.Name);
            }
            else if (!string.IsNullOrEmpty(Duration))
            {
                return u => u.OrderBy(i => i.Duration);
            }
            else if (!string.IsNullOrEmpty(Price))
            {
                return u => u.OrderBy(i => i.Price);
            }
            return null;
        }
    }
}