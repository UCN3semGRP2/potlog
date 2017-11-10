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
    }
}
