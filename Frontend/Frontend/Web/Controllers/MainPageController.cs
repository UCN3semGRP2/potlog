using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.ServiceReference;

namespace Web.Controllers
{
    public class MainPageController : Controller
    {
        public ActionResult PublicEvents()
        {
            return View();
        }

        public ActionResult SignedUpEvents()
        {
            User u = (User)Session["User"];
            return View(u);
        }

        public ActionResult CreatedEvents()
        {
            return View();
        }
    }
}