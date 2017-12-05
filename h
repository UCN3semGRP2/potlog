[1mdiff --git a/Backend/Backend/BLL/ComponentCtrl.cs b/Backend/Backend/BLL/ComponentCtrl.cs[m
[1mindex 306be1a..b6c338c 100644[m
[1m--- a/Backend/Backend/BLL/ComponentCtrl.cs[m
[1m+++ b/Backend/Backend/BLL/ComponentCtrl.cs[m
[36m@@ -48,5 +48,10 @@[m [mnamespace BLL[m
         {[m
             return catDB.FindComponentByParentId(id);[m
         }[m
[32m+[m
[32m+[m[32m        public void Update(Category c)[m
[32m+[m[32m        {[m
[32m+[m[32m            catDB.Update(c);[m
[32m+[m[32m        }[m
     }[m
 }[m
[1mdiff --git a/Backend/Backend/BLL/EventCtrl.cs b/Backend/Backend/BLL/EventCtrl.cs[m
[1mindex 64531c5..7cec81c 100644[m
[1m--- a/Backend/Backend/BLL/EventCtrl.cs[m
[1m+++ b/Backend/Backend/BLL/EventCtrl.cs[m
[36m@@ -58,9 +58,9 @@[m [mnamespace BLL[m
 [m
         public void SignUpForEvent(string userEmail, int eventId)[m
         {[m
[31m-                Event e = FindById(eventId);[m
[31m-                User u = uCtrl.FindByEmail(userEmail);[m
[31m-                RegisterToEvent(e, u);[m
[32m+[m[32m            Event e = FindById(eventId);[m
[32m+[m[32m            User u = uCtrl.FindByEmail(userEmail);[m
[32m+[m[32m            RegisterToEvent(e, u);[m
         }[m
 [m
         public void AddCategory(Event e, Category c)[m
[36m@@ -69,10 +69,27 @@[m [mnamespace BLL[m
             {[m
                 e.Components = new List<Component>();[m
             }[m
[31m-            e.Components.Add(c);[m
[31m-            c.Event = e;[m
[31m-            c.EventId = e.Id;[m
[31m-            eDB.Update(e);[m
[32m+[m[32m            if (c.Parent != null && c.Parent is Category)[m
[32m+[m[32m            {[m
[32m+[m[32m                // Should be moved to Component/category/item ctrl[m
[32m+[m[32m                var p = (Category)c.Parent;[m
[32m+[m[32m                p.Components.Add(c);[m
[32m+[m[32m                c.Event = p.Event;[m
[32m+[m[32m                c.EventId = p.EventId;[m
[32m+[m[32m                new ComponentCtrl().Update(c);[m
[32m+[m[32m                new ComponentCtrl().Update(p);[m
[32m+[m[32m                // ctx.Component.AddOrUpdate(p);[m
[32m+[m[32m                // ctx.Component.AddOrUpdate(c);[m
[32m+[m
[32m+[m[32m            }[m
[32m+[m[32m            else[m
[32m+[m[32m            {[m
[32m+[m[32m                e.Components.Add(c);[m
[32m+[m[32m                c.Event = e;[m
[32m+[m[32m                c.EventId = e.Id;[m
[32m+[m[32m                eDB.Update(e);[m
[32m+[m[32m            }[m
[32m+[m
         }[m
 [m
         public void AddItem(Event evnt, Category category, Item item)[m
[1mdiff --git a/Backend/Backend/BLLTest/EventCtrlTest.cs b/Backend/Backend/BLLTest/EventCtrlTest.cs[m
[1mindex 9731c97..93a7c82 100644[m
[1m--- a/Backend/Backend/BLLTest/EventCtrlTest.cs[m
[1m+++ b/Backend/Backend/BLLTest/EventCtrlTest.cs[m
[36m@@ -17,6 +17,9 @@[m [mnamespace BLLTest[m
         public void TestCreateEvent()[m
         {[m
             var ctrl = new EventCtrl();[m
[32m+[m[32m            UserCtrl uCtrl = new UserCtrl();[m
[32m+[m[32m            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");[m
[32m+[m
 [m
             Event e = new Event[m
             {[m
[36m@@ -27,11 +30,12 @@[m [mnamespace BLLTest[m
                 PriceTo = 200.0,[m
                 Location = "Sofiendalsvej 60",[m
                 Datetime = DateTime.Now.AddHours(1), //+1 hour from now to not trigger the past date exception[m
[31m-                IsPublic = true[m
[32m+[m[32m                IsPublic = true,[m
[32m+[m[32m                Admin = u[m
             };[m
 [m
             // Act[m
[31m-            Event output = ctrl.CreateEvent(e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, null);[m
[32m+[m[32m            Event output = ctrl.CreateEvent(e.Title, e.Description, e.NumOfParticipants, e.PriceFrom, e.PriceTo, e.Location, e.Datetime, e.IsPublic, u);[m
 [m
 [m
             // Assert[m
[36m@@ -82,7 +86,7 @@[m [mnamespace BLLTest[m
         {[m
             var user = new UserCtrl().CreateUser("1", "2", "test@test.test" + Guid.NewGuid(), "1234");[m
             var eCtrl = new EventCtrl();[m
[31m-            var e = eCtrl.CreateEvent("dsd", "dewdc", 23, 213.3, 21312.3, "here", DateTime.Now, false, user);[m
[32m+[m[32m            var e = eCtrl.CreateEvent("dsd", "dewdc", 23, 213.3, 21312.3, "here", DateTime.Now.AddHours(5), false, user);[m
 [m
 [m
             // Act[m
[36m@@ -104,8 +108,11 @@[m [mnamespace BLLTest[m
         {[m
             // Arrange[m
             EventCtrl eCtrl = new EventCtrl();[m
[32m+[m[32m            UserCtrl uCtrl = new UserCtrl();[m
[32m+[m[32m            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");[m
[32m+[m
             var e = eCtrl.CreateEvent("Event", "Evently event",[m
[31m-                2, 20, 100, "Right here", DateTime.Now, true, null);[m
[32m+[m[32m                2, 20, 100, "Right here", DateTime.Now, true, u);[m
             Category c = new ComponentCtrl().CreateCategory("Cat", "CateCat", null);[m
 [m
             //Act[m
[36m@@ -126,14 +133,16 @@[m [mnamespace BLLTest[m
             EventCtrl eCtrl = new EventCtrl();[m
             ComponentCtrl cCtrl = new ComponentCtrl();[m
             UserCtrl uCtrl = new UserCtrl();[m
[31m-            var u = uCtrl.CreateUser("Test User", "Test User", "test@email.com", "password");[m
[32m+[m[32m            var u = uCtrl.CreateUser("Test User", "Test User", "test"+Guid.NewGuid()+"@email.com", "password");[m
             var e = eCtrl.CreateEvent("Testing Event", "Test", 2, 20, 100, "Right here", DateTime.Now.AddHours(5), true, u);[m
             var c1 = cCtrl.CreateCategory("Testing Category Lvl 1", "Test", null);[m
[31m-            var c2 = cCtrl.CreateCategory("Testing Category Lvl 2", "Test", c1);[m
 [m
             // Act[m
             eCtrl.AddCategory(e, c1);[m
[31m-            eCtrl.AddCategory(e, c2);[m
[32m+[m
[32m+[m[32m            var c2 = cCtrl.CreateCategory("Testing Category Lvl 2", "Test", c1);[m
[32m+[m[32m            var e2 = eCtrl.FindById(e.Id);[m
[32m+[m[32m            eCtrl.AddCategory(e2, c2);[m
 [m
             Assert.IsTrue(true);[m
 [m
[36m@@ -150,8 +159,11 @@[m [mnamespace BLLTest[m
             // Arrange[m
             ComponentCtrl cCtrl = new ComponentCtrl();[m
             EventCtrl eCtrl = new EventCtrl();[m
[32m+[m[32m            UserCtrl uCtrl = new UserCtrl();[m
[32m+[m[32m            var u = uCtrl.CreateUser("Test User", "Test User", "test" + Guid.NewGuid() + "@email.com", "password");[m
[32m+[m
             // Act[m
[31m-            var evnt = eCtrl.CreateEvent("E Title", "E Desc", 42, 42, 42, "E Location", DateTime.Now.AddDays(5), true, null);[m
[32m+[m[32m            var evnt = eCtrl.CreateEvent("E Title", "E Desc", 42, 42, 42, "E Location", DateTime.Now.AddDays(5), true, u);[m
             var category = cCtrl.CreateCategory("Cat Name", "Cat desc", null);[m
             eCtrl.AddCategory(evnt, category);[m
             //var category2 = cCtrl.CreateCategory("Cat2 Name2", "Cat2 desc2", category);[m
[1mdiff --git a/Backend/Backend/BLLTest/RegistrationCtrlTest.cs b/Backend/Backend/BLLTest/RegistrationCtrlTest.cs[m
[1mindex 9800370..7c3c3a9 100644[m
[1m--- a/Backend/Backend/BLLTest/RegistrationCtrlTest.cs[m
[1m+++ b/Backend/Backend/BLLTest/RegistrationCtrlTest.cs[m
[36m@@ -20,7 +20,7 @@[m [mnamespace BLLTest[m
             UserCtrl uCtrl = new UserCtrl();[m
             var user = uCtrl.CreateUser("Jesper", "Jørgensen", "e@w.dk" + Guid.NewGuid(), "1234");[m
             EventCtrl eCtrl = new EventCtrl();[m
[31m-            var eve = eCtrl.CreateEvent("Hej", "nej", 5, 5.5, 6.5, "42", DateTime.Now, false, user);[m
[32m+[m[32m            var eve = eCtrl.CreateEvent("Hej", "nej", 5, 5.5, 6.5, "42", DateTime.Now.AddHours(5), false, user);[m
             RegistrationCtrl rCtrl = new RegistrationCtrl();[m
 [m
             // Act[m
[1mdiff --git a/Backend/Backend/DAL/CategoryDB.cs b/Backend/Backend/DAL/CategoryDB.cs[m
[1mindex 974053d..40f44fc 100644[m
[1m--- a/Backend/Backend/DAL/CategoryDB.cs[m
[1m+++ b/Backend/Backend/DAL/CategoryDB.cs[m
[36m@@ -5,6 +5,7 @@[m [musing System.Text;[m
 using System.Threading.Tasks;[m
 using Model;[m
 using System.Data.Entity;[m
[32m+[m[32musing System.Data.Entity.Migrations;[m
 [m
 namespace DAL[m
 {[m
[36m@@ -90,7 +91,11 @@[m [mnamespace DAL[m
 [m
         public void Update(Category entity)[m
         {[m
[31m-            throw new NotImplementedException();[m
[32m+[m[32m            using (var ctx = new DALContext())[m
[32m+[m[32m            {[m
[32m+[m[32m                ctx.Components.AddOrUpdate(entity);[m
[32m+[m[32m                ctx.SaveChanges();[m
[32m+[m[32m            }[m
         }[m
     }[m
 }[m
[1mdiff --git a/Backend/Backend/DAL/EventDB.cs b/Backend/Backend/DAL/EventDB.cs[m
[1mindex 8c14005..4a60387 100644[m
[1m--- a/Backend/Backend/DAL/EventDB.cs[m
[1m+++ b/Backend/Backend/DAL/EventDB.cs[m
[36m@@ -5,6 +5,7 @@[m [musing System.Linq;[m
 using System.Text;[m
 using System.Threading.Tasks;[m
 using System.Data.Entity;[m
[32m+[m[32musing System.Data.Entity.Migrations;[m
 [m
 namespace DAL[m
 {[m
[36m@@ -51,10 +52,22 @@[m [mnamespace DAL[m
         {[m
             using (DALContext ctx = new DALContext())[m
             {[m
[31m-                return ctx.Events[m
[31m-                    .Include(x => x.Registrations).Include(x => x.Components)[m
[32m+[m[32m                var e = ctx.Events[m
[32m+[m[32m                    .Include(x => x.Registrations).Include(x => x.Components).Include(x => x.Admin)[m
                     .Where(x => x.Id == id)[m
                     .First(); //.Find(id);[m
[32m+[m
[32m+[m[32m                //for (int i = 0; i < e.Components.Count; i++)[m
[32m+[m[32m                //{[m
[32m+[m[32m                //    var comp = e.Components[i];[m
[32m+[m[32m                //    if (comp is Item) continue;[m
[32m+[m[32m                //    e.Components[i] = ctx.Components.OfType<Category>()[m
[32m+[m[32m                //        .Where(x => x.Id == comp.Id)[m
[32m+[m[32m                //        .Include(x => x.Components)[m
[32m+[m[32m                //        .FirstOrDefault();[m
[32m+[m[32m                //}[m
[32m+[m
[32m+[m[32m                return e;[m
                 //return ctx.Events.Where(x => x.Id == id).Intersect()[m
             }[m
         }[m
[36m@@ -74,14 +87,16 @@[m [mnamespace DAL[m
                                 AttachComponent(ctx, comp);[m
                             }[m
                         }[m
[32m+[m[32m                        //ctx.Events.AddOrUpdate(entity);[m
                         ctx.Entry(entity).State = System.Data.Entity.EntityState.Modified;[m
[31m-                        [m
[32m+[m
                         ctx.SaveChanges();[m
                         ctxTransaction.Commit();[m
                     }[m
                     catch (Exception ex)[m
                     {[m
                         ctxTransaction.Rollback();[m
[32m+[m[32m                        Console.WriteLine(ex.StackTrace);[m
                         throw ex;[m
                     }[m
             }[m
[36m@@ -98,6 +113,7 @@[m [mnamespace DAL[m
                     {[m
                         ctx.Components.Attach(comp);[m
                         ctx.Entry(comp).State = System.Data.Entity.EntityState.Modified;[m
[32m+[m[32m                        ctx.Components.AddOrUpdate(comp);[m
                         if (comp is Category)[m
                         {[m
                             AttachComponent(ctx, comp);[m
[36m@@ -105,13 +121,10 @@[m [mnamespace DAL[m
                     }[m
                 }[m
             }[m
[31m-            else[m
[31m-            {[m
[31m-            }[m
[31m-            [m
[32m+[m
             ctx.Components.Attach(component);[m
             ctx.Entry(component).State = System.Data.Entity.EntityState.Modified;[m
[31m-            [m
[32m+[m
 [m
         }[m
     }[m
[1mdiff --git a/Backend/Backend/DAL/ItemDB.cs b/Backend/Backend/DAL/ItemDB.cs[m
[1mindex bb65453..2185ada 100644[m
[1m--- a/Backend/Backend/DAL/ItemDB.cs[m
[1m+++ b/Backend/Backend/DAL/ItemDB.cs[m
[36m@@ -15,6 +15,16 @@[m [mnamespace DAL[m
             {[m
                 using (var ctxTransaction = ctx.Database.BeginTransaction())[m
                 {[m
[32m+[m[32m                    if (entity.Parent != null)[m
[32m+[m[32m                    {[m
[32m+[m[32m                        ctx.Components.Attach(entity.Parent);[m
[32m+[m[32m                    }[m
[32m+[m
[32m+[m[32m                    if (entity.Event != null)[m
[32m+[m[32m                    {[m
[32m+[m[32m                        ctx.Events.Attach(entity.Event);[m
[32m+[m[32m                    }[m
[32m+[m
                     try[m
                     {[m
                         Item item = (Item)ctx.Components.Add(entity);[m
