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



        [HttpGet]
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
            var u = utils.Utils.GetUser(Session);

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
            var usr = utils.Utils.GetUser(Session);


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
                InviteString = inviteString,
                IsAlreadyRegistered = service.IsRegisteredToEvent(usr, e)
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

            var isAlreadyRegistered = service.IsRegisteredToEvent(utils.Utils.GetUser(Session), e);

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
                IsAlreadyRegistered = isAlreadyRegistered
            };

            ComponentModel cModel = new ComponentModel();
            cModel.EventId = e.Id;
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
                    cModel.CurrentLevelTwoItemId = null;
                    if (levelTwoComponents.Single(i => i.Id == (int)LevelTwoId) is Item)
                    {
                        cModel.CurrentLevelTwoItemId = LevelTwoId;
                        cModel.CurrentLevelThreeItemId = null;
                    }

                    var levelThreeComponents = service.FindComponentByParentId((int)LevelTwoId);
                    foreach (var item in levelThreeComponents)
                    {
                        cModel.LevelThreeList.Add(new SelectListItem { Text = item.Title, Value = item.Id.ToString() });
                    }

                    if (!(levelTwoComponents.Single(i => i.Id == (int)LevelTwoId) is Item))
                    {

                        if (LevelThreeId.HasValue)
                        {
                            if (levelThreeComponents.Single(i => i.Id == (int)LevelThreeId) is Item)
                            {
                                cModel.CurrentLevelThreeItemId = LevelThreeId;
                                cModel.CurrentLevelTwoItemId = null;
                            }
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
            User u = utils.Utils.GetUser(Session);
            if (u == null)
            {
                return RedirectToAction("LogIn", "User");
            }

            try
            {
                service.SignUpForEvent(u.Email, model.Id);
            }
            catch (FaultException fe)
            {
                ViewBag.ErrorMsg = fe.Message;
                return View();
            }

            return RedirectToAction("SignUpSuccess");
        }

        public ActionResult SignUpSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateCategory(DetailsEventViewModel model)
        {
            User u = (User)Session["User"];
            if (u == null)
            {
                return RedirectToAction("LogIn", "User");
            }
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
            var usr = utils.Utils.GetUser(Session);
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

            var usr = utils.Utils.GetUser(Session);
            if (usr == null)
            {
                return RedirectToAction("LogIn", "User");
            }

            var inviteString = model.InviteString;

            if (inviteString == null || inviteString == "")
            {
                ViewBag.ErrorMsg = errorMsg;
                return View();
            }

            Event evnt;
            try
            {
                evnt = service.AcceptInviteString(usr, inviteString);
                if (evnt == null)
                {
                    ViewBag.ErrorMsg = errorMsg;
                    return View();
                }
            }
            catch (FaultException fe)
            {
                ViewBag.ErrorMsg = fe.Message;
                return View();
            }


            return RedirectToAction("Details", "Event", new { id = evnt.Id });
        }

        public ActionResult SignUpForItem(ComponentModel model)
        {
            var usr = utils.Utils.GetUser(Session);
            if (usr == null)
            {
                return RedirectToAction("LogIn", "User");
            }

            try
            {
                if (model.CurrentLevelTwoItemId != null)
                {
                    service.SignUpForItem(usr.Email, (int)model.CurrentLevelTwoItemId);
                    ViewBag.SuccessMessage = "Registrering er nu opdateret.";
                }
                else if (model.CurrentLevelThreeItemId != null)
                {
                    service.SignUpForItem(usr.Email, (int)model.CurrentLevelThreeItemId);
                    ViewBag.SuccessMessage = "Registrering er nu opdateret.";
                }

                Event e = service.FindEventById(model.EventId);
                DetailsEventViewModel ev = CreateDetailsForEvent(e, usr);
                

                return View("Details", ev);
            }
            catch (FaultException fax)
            {
                Event e = service.FindEventById(model.EventId);
                DetailsEventViewModel ev = CreateDetailsForEvent(e, usr);

                ViewBag.Message = fax.Message;
                return View("Details", ev);
            }


        }
        public DetailsEventViewModel CreateDetailsForEvent(Event e, User usr)
        {
            ComponentModel cModel = new ComponentModel();
            cModel.EventId = e.Id;
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

            DetailsEventViewModel ev = new DetailsEventViewModel
            {
                ComponentModel = cModel,
                Date = e.Datetime.Date,
                Description = e.Description,
                Id = e.Id,
                InviteString = (usr.Id == e.Admin.Id) ? service.GetInviteString(e, usr) : null,
                IsPublic = e.IsPublic,
                Location = e.Location,
                NumOfParticipants = e.NumOfParticipants,
                PriceFrom = e.PriceFrom,
                PriceTo = e.PriceTo,
                Time = e.Datetime.TimeOfDay,
                Title = e.Title
            };

            return ev;
        }
    }
}
