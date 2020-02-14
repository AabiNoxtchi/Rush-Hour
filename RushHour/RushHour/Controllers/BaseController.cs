using DataAccess.Entity;
using DataAccess.Repository;
using RushHour.ViewModels.Home;
using RushHour.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace RushHour.Controllers
{
    public abstract class BaseController<E, R, FilterVM, IndexVM, EditVM, OrderBy> : Controller
        where E : BaseEntity, new()
        where R : BaseRepository<E>, new()
        where IndexVM : BaseIndexVM<E, FilterVM, OrderBy>, new()
        where FilterVM : BaseFilterVM<E>, new()
        where EditVM : BaseEditVM<E>, new()
        where OrderBy : BaseOrderBy<E>, new()
    {
        protected virtual void PopulateModel(IndexVM model) { }
        protected virtual void PopulateEditModel(EditVM model, ref bool valid) { }
        protected virtual void PopulateRelatedEntities(IndexVM model) { }   


        [HttpGet]
        public virtual ActionResult Index(IndexVM model)
        {
            model.Pager = model.Pager ?? new PagerVM();
            model.Pager.Prefix = "Pager";
            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Filter = model.Filter ?? new FilterVM();
            model.Filter.Prefix = "Filter";
            PopulateModel(model);
            Expression<Func<E, bool>> filter = model.Filter.GenerateFilter();

            model.OrderBy = model.OrderBy ?? new OrderBy();
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy = model.OrderBy.orderBy();
            R repo = new R();
            model.Items = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage, orderBy);

            model.Pager.PagesCount = (int)Math.Ceiling(repo.Count(filter) / (double)(model.Pager.ItemsPerPage));

            PopulateRelatedEntities(model);

            return View(model);
            
        }

       

        [HttpGet]
        public virtual ActionResult Edit(EditVM model, FormCollection form)
        {
            ModelState.Clear();

            model = model ?? new EditVM();

            bool valid = true;

            if (model.Id > 0)
            {
                E item = null;
                R repo = new R();
                item = repo.GetById(model.Id);

                if (item == null)
                {
                    valid = false;
                }
                else
                {
                    model.PopulateModel(item);                   
                }
            }

            PopulateEditModel(model, ref valid);

            if (!valid)
            {
                ModelState.AddModelError("", "The record you attempted to Edit not found");
                return View(model);
            }
           
            return View(model);
        }

        [HttpPost]
        public virtual ActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            R repo = new R();
            E item = new E();

            bool valid = true;
            if (model.Id > 0)
            {
                item = repo.GetById(model.Id);
                if (item == null)
                {
                    valid = false;
                }
            }
           
            PopulateEditModel(model, ref valid);

            if (!valid)
            {
                ModelState.AddModelError("", "The record you attempted to Edit or not found");
                return View(model);
            }
            model.PopulateEntity(item);

            try
            {
                repo.Save(item);
            }
            catch (Exception)
            {               
                return RedirectToAction("Login", "Home");
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public virtual ActionResult Delete(int id)
        {
            R repo = new R();
            E item = repo.GetById(id);
            bool valid = true;
           
               
                if (item == null)
                {
                    valid = false;
                }

            if (!valid)
            {
                return RedirectToAction("Index");
            }
           
                repo.Delete(item);       

            return RedirectToAction("Index");
        }

       
       
    }
}