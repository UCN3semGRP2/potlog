using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.ServiceReference;

namespace Web.Controllers.utils
{
    public class Utils
    {
        private  static ServiceReference.IService service = new ServiceReference.ServiceClient();

        public static User GetUser(HttpSessionStateBase session)
        {
            var usr = (User)session["User"];
            if (usr == null) return null;
            usr = service.UpdateUserInfo(usr);
            session["User"] = usr;
            return usr;
        }
    }
}