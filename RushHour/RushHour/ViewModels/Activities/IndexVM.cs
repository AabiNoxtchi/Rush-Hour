using DataAccess.Entity;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RushHour.ViewModels.Activities
{
    public class IndexVM : BaseIndexVM<Activity,FilterVM,OrderBy>
    {
        public Appointments.IndexVM AppointmentsIndexVM { get; set; }
    }
}