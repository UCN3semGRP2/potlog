using System;
using Model;

namespace BLL
{
    public class SessionCtrl
    {
        public SessionCtrl()
        {
        }

        public bool IsValidated(Session ses)
        {
            if (ses == null) return false;

            var diff = ses.ExpireDate.Subtract(DateTime.Now);

            if (diff.TotalSeconds < 0)
            {
                return false;
            }

            return true;
        }
    }
}