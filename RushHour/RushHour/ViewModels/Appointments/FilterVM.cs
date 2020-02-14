using DataAccess.Entity;
using RushHour.Attributes.FilterVM;
using RushHour.Models;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RushHour.ViewModels.Appointments
{
    public class FilterVM : BaseFilterVM<Appointment>
    {
        [Skip]
        public int UserId { get; set; }

       
        [Skip]
        public Nullable<int> ActivityId { get; set; }


        [DropDownFilter(TargetModelProperty = "ActivityId", DataProperty = "Value", DisplayProperty = "Text")]
        public SelectList activitiesList { get; set; } 

        [DateTime]
        [DisplayName("Start Date and Time")]
        public Nullable<DateTime> StartDateTime { get; set; }

        [DateTime]
        [DisplayName("End Date And Time")]
        public Nullable<DateTime> EndDateTime { get; set; }



        public override Expression<Func<Appointment, bool>> GenerateFilter()
        {
            if (AuthenticationManager.LoggedUser.IsAdmin)
            {
                if (ActivityId > 0)
                {
                    return i => ((StartDateTime == null) || i.StartDateTime >= StartDateTime) &&
                                (EndDateTime == null || i.EndDateTime <= EndDateTime) &&
                                (i.ActivityId == ActivityId);
                }
                else
                {
                   
                    return i => ((StartDateTime == null) || i.StartDateTime >= StartDateTime) &&
                               (EndDateTime == null || i.EndDateTime <= EndDateTime) &&
                               (UserId==0?true:i.UserId==UserId) &&
                              (ActivityId == null || i.ActivityId == ActivityId);
                }                         
            }
            else
            {
                return i => ((StartDateTime == null) || i.StartDateTime >= StartDateTime) &&
                            (EndDateTime == null || i.EndDateTime<=EndDateTime) &&
                            (i.UserId == UserId) &&
                            (ActivityId == null || i.ActivityId == ActivityId);
            }
        }
    }
}