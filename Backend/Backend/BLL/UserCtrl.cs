using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace BLL
{
    public class UserCtrl
    {
        public bool ValidatePassword(User u, string clearTextPw)
        {
            return HashingHelper.CheckPassword(clearTextPw, u.Salt, u.Password);
        }

        public bool IsValidated(User u1)
        {
            var ses = u1.LogInSession;
            if (ses == null) return false;

            var diff = DateTime.Now.Subtract(ses.ExpireDate);

            if (diff.TotalHours < 4 * 60)
            {
                return false;
            }

            return true;
        }
    }
}
