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

            var diff = ses.ExpireDate.Subtract(DateTime.Now);

            Console.WriteLine("ses:");
            Console.WriteLine(ses.ExpireDate);

            Console.WriteLine("diff:");
            Console.WriteLine(diff);

            Console.WriteLine("total hours:");
            Console.WriteLine(diff.TotalHours);

            Console.WriteLine("total seconds:");
            Console.WriteLine(diff.TotalSeconds);
            if (diff.TotalSeconds > 0)
            {
                return false;
            }

            return true;
        }
    }
}
