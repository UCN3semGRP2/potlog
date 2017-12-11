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
            return RedirectToAction("Create", "Event");
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
                return RedirectToAction("Details", new { id = evnt.Id});
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
            var usr = (User)Session["User"];

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var e = service.FindEventById(id.Value);

            var inviteString = (usr.Id == e.Admin.Id) ? service.GetInviteString(e, usr) : null;
            

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
                Title = e.Title,
                InviteString = inviteString
                //AllComponents = e.Components
            };
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

        [HttpGet]
        public ActionResult InvitationString()
        {
            var usr = (User)Session["User"];
            if (usr == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            return View();
        }

        [HttpPost]
        public ActionResult InvitationString(InviteStringViewModel model)
        {
            const string errorMsg = "Den indtastede invitationskode er ikke gyldig";

            var usr = (User)Session["User"];
            if (usr == null)
            {
                return RedirectToAction("LogIn", "User");
            }
            // TODO redirect to the found event

            var inviteString = model.InviteString;

            if (inviteString == null || inviteString == "")
            {
                // Show an error
                ViewBag.ErrorMsg = errorMsg;
                return View();
            }

            
            var evnt = service.AcceptInviteString(usr, inviteString);
            if (evnt == null)
            {
                ViewBag.ErrorMsg = errorMsg;
                return View();
            }


            return RedirectToAction("Details", "Event", new { id = evnt.Id });
        }

    }
}