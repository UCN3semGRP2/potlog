using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web.Models;

namespace Web.Controllers
{
    public class UserController : Controller
    {
        private const string SessionGUIDCookie = "SessionGUID";

        ServiceReference.IService service = new ServiceReference.ServiceClient();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            var sesCookie = Request.Cookies[SessionGUIDCookie]; // Example of extracting the cookie
            

            return View();
        }

        [HttpPost]
        public ActionResult LogIn(UserLogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var user = service.LogIn(model.Email, model.Password);
            if (user == null)
            {
                var errMsg = "Email eller password er ikke korrekt";
                ModelState.AddModelError("", errMsg);
                return View(model);
            }

            Response.Cookies[SessionGUIDCookie].Value = user.LogInSession.GUID.ToString(); 
            return RedirectToAction("Index"); // TODO: redirect to the correct view
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

            service.CreateUser(model.Firstname, model.Lastname, model.Email, model.Password);

            //TODO
            return RedirectToAction("TODO");
        }
    }
}