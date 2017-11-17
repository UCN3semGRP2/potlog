using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class EventController : Controller
    {
        ServiceReference.IService service = new ServiceReference.ServiceClient();

        // GET: Event
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
        public ActionResult Create(CreateEventViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            DateTime dt = model.Date + model.Time;

            var evnt = service.CreateEvent(model.Title, model.Description, model.NumOfParticipants, model.PriceFrom, model.PriceTo, model.Location, dt, model.IsPublic);

            return RedirectToAction("CreateSuccess");
        }

        [HttpGet]
        public ActionResult CreateSuccess()
        {
            return View();
        }
    }
}