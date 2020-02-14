using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RushHour.ViewModels.Appointments
{
    public class IndexVM : BaseIndexVM<Appointment, FilterVM,OrderBy>
    {
        public User user { get; set; }

    }
}