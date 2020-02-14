using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Activities
{
    public class EditVM : BaseEditVM<Activity>
    {

        [DisplayName("Name")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Name { get; set; }

       
        [DisplayName("Duration")]
        [Required(ErrorMessage = "This field is Required!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public Nullable<double> Duration { get; set; }


        [DisplayName("Price")]        
        [Required(ErrorMessage = "This field is Required!")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:C}")]
        public Nullable<decimal> Price { get; set; }

        public override void PopulateEntity(Activity item)
        {
            item.Id = Id;
            item.Name = Name;
            item.Duration = Duration??0;
            item.Price = Price??0;
        }

        public override void PopulateModel(Activity item)
        {
            Id = item.Id;
            Name = item.Name;
            Duration = item.Duration;
            Price = item.Price;
        }
    }
}