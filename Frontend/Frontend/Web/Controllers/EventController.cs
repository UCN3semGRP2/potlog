using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.ServiceReference;

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
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Create(CreateEventViewModel model)
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            var u = (User)Session["User"];

            DateTime dt = model.Date + model.Time;

            try
            {
                var evnt = service.CreateEvent(model.Title, model.Description, model.NumOfParticipants, model.PriceFrom, model.PriceTo, model.Location, dt, model.IsPublic, u);
                return RedirectToAction("Details", new { id = evnt.Id });
            }
            catch (FaultException err)
            {
                ViewBag.Message = err.Message;
                return View();
            }

        }

        [HttpGet]
        public ActionResult CreateSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (Session["LoggedIn"] == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = service.FindEventById(id.Value);
            DetailsEventViewModel ev = new DetailsEventViewModel
            {
                Id = e.Id,
                Date = e.Datetime.Date,
                Description = e.Description,
                IsPublic = e.IsPublic,
                Location = e.Location,
                NumOfParticipants = e.NumOfParticipants,
                PriceFrom = e.PriceFrom,
                PriceTo = e.PriceTo,
                Time = new TimeSpan(e.Datetime.Hour, e.Datetime.Minute, e.Datetime.Second),
                Title = e.Title
            };

            ComponentModel cModel = new ComponentModel();

            // Top level category
            foreach (var item in e.Components)
            {
                if (item.Parent == null)
                {
                    cModel.LevelOneList.Add(new SelectListItem
                    {
                        Text = item.Title,
                        Value = item.Id.ToString()
                    });
                }
            }

            foreach (var item in e.Components)
            {
                if (item.Parent != null)
                {
                    var test = cModel.LevelOneList.Find(i => Int32.Parse(i.Value) == item.Parent.Id);
                    if (test != null)
                    {
                        cModel.LevelTwoList.Add(new SelectListItem
                        {
                            Text = item.Title,
                            Value = item.Id.ToString()
                        });
                    }
                }
            }

            ev.ComponentModel = cModel;

            return View(ev);
        }

        [HttpPost]
        public ActionResult SignUp(DetailsEventViewModel model)
        {
            User u = (User)Session["User"];
            if (u == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            service.SignUpForEvent(u.Email, model.Id);

            return RedirectToAction("SignUpSuccess");
        }

        public ActionResult SignUpSuccess()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateCategory(DetailsEventViewModel model)
        {
            int id = model.Id;
            return View(new CreateComponentViewModel
            {
                EventId = id,
                Title = "",
                Description = ""
            });
        }
        [HttpPost]
        public ActionResult CreateCategory(CreateComponentViewModel model)
        {
            // TODO Methods must handle parents.
            service.AddCategoryToEvent(model.EventId, model.Title, model.Description, null);
            return RedirectToAction("Details", new { id = model.EventId });
        }

    }
}