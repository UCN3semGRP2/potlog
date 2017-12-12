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
            };

            ComponentModel cModel = new ComponentModel();

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

            ev.ComponentModel = cModel;

            return View(ev);
        }

        [HttpPost]
        public ActionResult Details(int? eventId, int? LevelOneId, int? LevelTwoId, int? LevelThreeId)
        {
            var e = service.FindEventById(eventId.Value);
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

            if (LevelOneId.HasValue)
            {
                var levelTwoComponents = service.FindComponentByParentId((int)LevelOneId);
                foreach (var item in levelTwoComponents)
                {
                    cModel.LevelTwoList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
                }

                if (LevelTwoId.HasValue)
                {
                    if (levelTwoComponents.Single(i => i.Id == (int)LevelTwoId) is Item)
                    {
                        cModel.IsItem = true;
                        cModel.currentItemId = LevelTwoId;
                        //TODO: Need to create button in UI for this.
                    }

                    var levelThreeComponents = service.FindComponentByParentId((int)LevelTwoId);
                    foreach (var item in levelThreeComponents)
                    {
                        cModel.LevelThreeList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
                    }

                    if (LevelThreeId.HasValue)
                    {
                        if (levelThreeComponents.Single(i => i.Id == (int)LevelThreeId) is Item)
                        {
                            cModel.IsItem = true;
                            cModel.currentItemId = LevelThreeId;
                            //TODO: Need to create button in UI for this.
                        }
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
            var evnt = service.FindEventById(model.Id);

            CreateComponentViewModel viewModel = new CreateComponentViewModel
            {
                EventId = id,
                Title = "",
                Description = ""
            };

            List<SelectListItem> categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Text = "Ingen", Value = null });

            foreach (var item in evnt.Components)
            {
                if (item is Category && item.Parent == null)
                {

                    categories.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
                }
            }

            viewModel.Categories = categories;

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult CreateCategory(CreateComponentViewModel model)
        {
            int parentId;
            string selectedCategory = model.SelectedCategory;

            if (!(selectedCategory == null || selectedCategory == "" || selectedCategory == "Ingen"))
            {
                Int32.TryParse(selectedCategory, out parentId);
                var component = service.FindCategoryById(parentId);
                service.AddCategoryToEvent(model.EventId, model.Title, model.Description, component);

            }
            else
            {
                service.AddCategoryToEvent(model.EventId, model.Title, model.Description, null);
            }


            return RedirectToAction("Details", new { id = model.EventId });
        }

        [HttpGet]
        public ActionResult CreateItem(DetailsEventViewModel model)
        {
            int id = model.Id;
            var evnt = service.FindEventById(model.Id);

            CreateItemViewModel viewModel = new CreateItemViewModel
            {
                EventId = id,
                Title = "",
                Description = ""
            };

            List<SelectListItem> categories = new List<SelectListItem>();
            foreach (var item in evnt.Components)
            {
                if (item is Category)
                {

                    categories.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
                }
            }

            viewModel.Categories = categories;

            return View(viewModel);

        }

        [HttpPost]
        public ActionResult CreateItem(CreateItemViewModel model)
        {
            int parentId;
            string selectedCategory = model.SelectedCategory;

            if (selectedCategory != null || selectedCategory != "")
            {
                Int32.TryParse(selectedCategory, out parentId);
                service.AddItemToCategory(model.EventId, parentId, model.Amount, model.Title, model.Description);
            }

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