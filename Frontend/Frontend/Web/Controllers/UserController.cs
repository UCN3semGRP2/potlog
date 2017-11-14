using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

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