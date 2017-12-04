using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using Web.Models;
using Web.ServiceReference;

namespace Web.Controllers
{
    public class UserController : Controller
    {

        ServiceReference.IService service = new ServiceReference.ServiceClient();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            Session.Clear();
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLogInViewModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = service.LogIn(model.Email, model.Password);
            if (user == null)
            {
                var errMsg = "Email eller password er ikke korrekt";
                ModelState.AddModelError("", errMsg);
                return View(model);
            }

            Session["LoggedIn"] = true;
            Session["User"] = user; 
            return RedirectToAction("SignedUpEvents", "MainPage"); 
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            try
            {
                service.CreateUser(model.Firstname, model.Lastname, model.Email, model.Password);
                ViewBag.SuccessMessage = "Bruger er nu oprettet";
                return View();
            }
            catch (FaultException fax)
            {
                ViewBag.Message = fax.Message;
                return View();
                
            }
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            User u = (User)Session["User"];
            EditUserViewModel euvm = new EditUserViewModel
            {
                id = u.Id,
                Email = u.Email,
                Firstname = u.Firstname,
                Lastname = u.Lastname
            };
            return View(euvm);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            User u = new User {
                Firstname = model.Firstname,
                Lastname = model.Lastname,
                Email = model.Email,
                Id = model.id,
                Password = model.Password                
            };
            //Todo service.UpdateUser(u);
            ViewBag.SuccessMessage = "Bruger er nu opdateret";
            return View();
            //return RedirectToAction("SignedUpEvents", "MainPage");
        }
    }
}