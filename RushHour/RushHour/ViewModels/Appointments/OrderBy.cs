using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Appointments
{
    public class OrderBy:BaseOrderBy<Appointment>
    {
        public string Title { get; set; }
        public string StartDateTime { get; set; }
        public string EndDateTime { get; set; }
        public override Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> orderBy()
        {
            if (!string.IsNullOrEmpty(Title))
            {
                return u => u.OrderBy(i => i.Title);
            }
            else if (!string.IsNullOrEmpty(StartDateTime))
            {
                return u => u.OrderBy(i => i.StartDateTime);
            }
            else if (!string.IsNullOrEmpty(EndDateTime))
            {
                return u => u.OrderBy(i => i.EndDateTime);
            }
            return null;
        }
    }
}