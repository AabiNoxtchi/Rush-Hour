using DataAccess.Entity;
using DataAccess.Repository;
using DataAccess.Service;
using RushHour.Models;
using RushHour.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace RushHour.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Register()
        {
            RegisterVM model = new RegisterVM();            
            model.statusVM = model.statusVM ?? new StatusVM();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            UsersRepository repo = new UsersRepository();

            if (repo.GetAll(u => u.Email == model.Email).FirstOrDefault() != null)
            {
                ModelState.AddModelError("EmailExist", "A Profile with this Email Already Exist");
                return View(model);
            }
            else
            {
                model.IsEmailVerified = false;

                User item = new User();
                model.PopulateEntity(item);
                item.ActivationCode =Guid.NewGuid();
                repo.Save(item);

                SendEmail(item);
               
                model.statusVM.message = "Registration successfully done,activation link has bees sent to  "  ;                
                model.statusVM.status = true;

                return View(model);
            }
        }

        private void SendEmail(User item)
        {
            var verifyURL = "/Home/VerifyAccount/" + item.ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyURL);

            string subject = "Your Account is successfully created";

            string body = "<br/><br/>Your account is successfully created,Please verify your email address by clicking the link below." +
                "<br/><br/><a href ='" + link + "'>" + link + "</a> ";

            MailService.SendEmailVerification(item.Email, subject, body);
        }


        [HttpGet]
        public ActionResult VerifyAccount(string id)
        {
            StatusVM model = new StatusVM();
            UsersRepository usersRepo = new UsersRepository();
            User user = usersRepo.GetAll(u => u.ActivationCode == new Guid(id)).FirstOrDefault();

            if (user != null)
            {
                user.IsEmailVerified = true;
                usersRepo.Save(user);
                model.status = true;
            }
            else
            {
                model.message = "Invalid Request ,Verification Is Not Complete!";
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (this.ModelState.IsValid)
            {
                Models.AuthenticationManager.Authenticate(model.Email, model.Password);

                if (Models.AuthenticationManager.LoggedUser == null)
                    ModelState.AddModelError("authenticationFailed", "Wrong username or password!");
            }

            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            Models.AuthenticationManager.Logout();

            return RedirectToAction("Login", "Home");
        }


       

    }
}