using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            return View();
        }

        public ActionResult CreatedEvents()
        {
            return View();
        }
    }
}