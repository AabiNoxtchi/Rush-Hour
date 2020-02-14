using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Appointments
{
    public class EditVM : BaseEditVM<Appointment>
    {

        public int UserId { get; set; }

        public int ActivityId { get; set; }

        public string Title { get; set; }

        [DisplayName("Start Date and Time:")]
        [Required(ErrorMessage = "This field is Required!")]
        [DataType(DataType.DateTime)]
        public Nullable<DateTime> StartDateTime { get; set; }

        [DisplayName("End Date and Time:")]
        [ReadOnly(true)]
        public Nullable<DateTime> EndDateTime { get; set; }

       
        [Timestamp]
        public byte[] RowVersion { get; set; }

        public Activity activity { get; set; }


        public override void PopulateEntity(Appointment item)
        {
            item.Id = Id;
            item.UserId = UserId;
            item.Title = Title;
            item.ActivityId = ActivityId;
            item.StartDateTime = StartDateTime??DateTime.MinValue;
            item.EndDateTime = EndDateTime??DateTime.MinValue;
        }

        public override void PopulateModel(Appointment item)
        {
            Id = item.Id;
            UserId = item.UserId;
            Title = item.Title;
            ActivityId = item.ActivityId;
            StartDateTime = item.StartDateTime;
            EndDateTime = item.EndDateTime;
        }
    }
}