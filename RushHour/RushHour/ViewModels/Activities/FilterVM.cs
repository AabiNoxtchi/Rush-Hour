using DataAccess.Entity;
using RushHour.Attributes.FilterVM;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RushHour.ViewModels.Activities
{
    public class FilterVM : BaseFilterVM<Activity>
    {

        [Skip]
        public string Name { get; set; }

        [DisplayName("Name")]
        [DropDownFilter(TargetModelProperty = "Name", DisplayProperty = "Text", DataProperty = "Value")]
        public SelectList NameList { get; set; }


        [Skip]
        public Nullable<double> Duration { get; set; }

        [DisplayName("Max Duration")]
        [DropDownFilter(TargetModelProperty = "Duration", DisplayProperty = "Text", DataProperty = "Value")]
        public SelectList DurationList { get; set; }

        [Skip]  
        public Nullable<decimal> Price { get; set; }

        [DisplayName("Max Price")]        
        [DropDownFilter(TargetModelProperty ="Price",DisplayProperty ="Text",DataProperty ="Value")]
        public SelectList PriceList { get; set; }

        public override Expression<Func<Activity, bool>> GenerateFilter()
        {
            return i => (string.IsNullOrEmpty(Name) || i.Name==Name) &&
                       ( Duration==null || i.Duration <= Duration) &&
                       ( Price == null || i.Price <= Price);
                     

        }

            
    }
}