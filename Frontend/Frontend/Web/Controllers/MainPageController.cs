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
        // TODO: At all places that we display something about the user that is logged in we should get a fresh user object
        //       from the service so that we know that the data is updated.

        public ActionResult Index()
        {
            return RedirectToAction("SignedUpEvents");
        }

        public ActionResult Home()
        {
            return RedirectToAction("SignedUpEvents");
        }

        public ActionResult PublicEvents()
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            return View();
        }

        public ActionResult SignedUpEvents()
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            User u = (User)Session["User"];
            return View(u);
        }

        public ActionResult CreatedEvents()
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            return View();
        }
    }
}