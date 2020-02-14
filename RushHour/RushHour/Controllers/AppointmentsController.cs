using DataAccess.Entity;
using DataAccess.Repository;
using RushHour.Attributes.FilterVM;
using RushHour.Filters;
using RushHour.Models;
using RushHour.ViewModels.Appointments;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RushHour.Controllers
{
   
    [AuthenticationFilter]
    public class AppointmentsController :BaseController<Appointment, AppointmentsRepository, FilterVM, IndexVM, EditVM,OrderBy>
    {
        protected override void PopulateModel(IndexVM model)
        {
            if (!AuthenticationManager.LoggedUser.IsAdmin)
            {
                model.Filter.UserId = AuthenticationManager.LoggedUser.Id;
            }
            else 
            {
                UsersRepository uRepo = new UsersRepository();
                model.user = uRepo.GetById(model.Filter.UserId);
            }


            if (model.Filter.UserId == 0)
            {
                model.Filter.UserId = AuthenticationManager.LoggedUser.Id;
            }           


            ActivitiesRepository aRepo = new ActivitiesRepository();
            List<Activity> dbActivities = aRepo.GetAll(a => true);

            List<SelectListItem> listItems = new List<SelectListItem>();

            listItems.Add(new SelectListItem()
            {
                Text = "",
                Value = null

            });

            foreach (Activity activity in dbActivities)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = activity.Name,
                    Value = activity.Id.ToString()

                });

            }

            model.Filter.activitiesList = new SelectList(listItems, "Value", "Text", model.Filter.ActivityId ?? null);

        }

       

        protected override void PopulateEditModel(EditVM model,ref bool valid)
        {
            if (valid == true)
            {
                if (!AuthenticationManager.LoggedUser.IsAdmin)
                {
                    model.UserId = AuthenticationManager.LoggedUser.Id;
                }

                if (model.UserId == 0)
                {
                    model.UserId = AuthenticationManager.LoggedUser.Id;
                }

                ActivitiesRepository repo = new ActivitiesRepository();
                model.activity = repo.GetById(model.ActivityId);
                if (model.activity != null)
                {
                    model.Title = model.activity.Name;
                    if (model.StartDateTime != null)
                    {
                        TimeSpan duration = TimeSpan.FromHours(model.activity.Duration);
                        model.EndDateTime = model.StartDateTime.Value.Add(duration);
                    }
                }
                else
                {
                    valid = false;
                }
            }

        }


        public override ActionResult Index(IndexVM model)
        {
            model.Filter = model.Filter ?? new FilterVM();
            model.Filter.Prefix = "Filter";
            PopulateModel(model);
            Expression<Func<Appointment, bool>> filter = model.Filter.GenerateFilter();

            AppointmentsRepository repo = new AppointmentsRepository();
            model.Items = repo.GetAll(filter);
           
            return View(model);
        }

    }
}