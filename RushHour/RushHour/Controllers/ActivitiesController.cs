using DataAccess.Entity;
using DataAccess.Repository;
using RushHour.Filters;
using RushHour.Models;
using RushHour.ViewModels.Activities;
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
    public class ActivitiesController : BaseController<Activity, ActivitiesRepository, FilterVM, IndexVM, EditVM,OrderBy>
    {
        protected override void PopulateModel(IndexVM model)
        {
            ActivitiesRepository repo = new ActivitiesRepository();
            List<Activity> dbActivities = repo.GetAll(a => true);

            List<SelectListItem> NamelistItems = PopulateSelectList();
            foreach (Activity activity in dbActivities)
            {
                NamelistItems.Add(new SelectListItem()
                {
                    Text = activity.Name,
                    Value = activity.Name

                });
            }
            model.Filter.NameList = new SelectList(NamelistItems, "Value", "Text", model.Filter.Name ?? null);


            List<SelectListItem> pricelistItems = PopulateSelectList();          
           int priceMax = (int)Math.Ceiling(dbActivities.Max(a => (double)a.Price))+100;           
            
            for (int i = 100; i <=priceMax; i += 100)
            {
                pricelistItems.Add(new SelectListItem()
                {
                    Text = String.Format("{0:C}", i),
                    Value = i.ToString()
                });
            }
            model.Filter.PriceList = new SelectList(pricelistItems, "Value", "Text", model.Filter.Price ?? null);


            List<SelectListItem> durationlistItems = PopulateSelectList();           
            int durationMax = (int)Math.Ceiling(dbActivities.Max(a => a.Duration))+2;

            for (int i = 2; i <=durationMax ; i+=2)
            {
                durationlistItems.Add(new SelectListItem()
                {
                    Text = i.ToString("0.00")+"h",
                    Value = i.ToString()
                });
            }
            model.Filter.DurationList = new SelectList(durationlistItems, "Value", "Text", model.Filter.Duration ?? null);
        }


        private List<SelectListItem> PopulateSelectList()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems.Add(new SelectListItem()
            {
                Text = "",
                Value = null

            });
            return listItems;
        }

        protected override void PopulateRelatedEntities(IndexVM model)
        {
            model.AppointmentsIndexVM = model.AppointmentsIndexVM ?? new ViewModels.Appointments.IndexVM();
            model.AppointmentsIndexVM.Filter = model.AppointmentsIndexVM.Filter ?? new ViewModels.Appointments.FilterVM();
            model.AppointmentsIndexVM.Filter.Prefix = "AppointmentsIndexVM.Filter";           

            model.AppointmentsIndexVM.Pager = model.AppointmentsIndexVM.Pager ?? new PagerVM();
            model.AppointmentsIndexVM.Pager.Prefix = "AppointmentsIndexVM.Pager";
            model.AppointmentsIndexVM.Pager.Page = model.AppointmentsIndexVM.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.AppointmentsIndexVM.Pager.ItemsPerPage = model.AppointmentsIndexVM.Pager.ItemsPerPage <= 0 ? 10 : model.AppointmentsIndexVM.Pager.ItemsPerPage;


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

            model.AppointmentsIndexVM.Filter.activitiesList = new SelectList(listItems, "Value", "Text", model.AppointmentsIndexVM.Filter.ActivityId.ToString()?? null);

            Expression<Func<Appointment, bool>> Appointmentsfilter = model.AppointmentsIndexVM.Filter.GenerateFilter();        

            model.AppointmentsIndexVM.OrderBy = model.AppointmentsIndexVM.OrderBy ?? new ViewModels.Appointments.OrderBy();
            Func<IQueryable<Appointment>, IOrderedQueryable<Appointment>> AppointmentsorderBy = model.AppointmentsIndexVM.OrderBy.orderBy();
            AppointmentsRepository repo = new AppointmentsRepository();

            model.AppointmentsIndexVM.Items = repo.GetAll(Appointmentsfilter, model.AppointmentsIndexVM.Pager.Page, model.AppointmentsIndexVM.Pager.ItemsPerPage, AppointmentsorderBy);
            model.AppointmentsIndexVM.Pager.PagesCount = (int)Math.Ceiling(repo.Count(Appointmentsfilter) / (double)(model.AppointmentsIndexVM.Pager.ItemsPerPage));
        }
       


        public override ActionResult Index(IndexVM model)
        {
            return base.Index(model);
        }

        [HttpGet]
        [AuthorizeAdmin]
        public override ActionResult Edit(EditVM model, FormCollection form)
        {
            return base.Edit(model, form);
        }


        [HttpPost]
        [AuthorizeAdmin]
        public override ActionResult Edit(EditVM model)
        {
            return base.Edit(model);
        }

        [AuthorizeAdmin]
        public override ActionResult Delete(int id)
        {
            return base.Delete(id);
        }
    }
}