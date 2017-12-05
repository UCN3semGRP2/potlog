using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class UserCtrl
    {
        private SessionCtrl SesCtrl = new SessionCtrl();
        private IUserDB uDB = new UserDB();
        public User CreateUser(string Firstname, string Lastname, string Email, string Password)
        {

            string salt = HashingHelper.GenerateSalt();
            string hashedPassword = HashingHelper.HashPassword(Password, salt);

            User user = new User
            {
                Firstname = Firstname,
                Lastname = Lastname,
                Email = Email,
                Password = hashedPassword,
                Salt = salt
            };
            var enduser = uDB.Create(user);
            if (enduser == null)
            {
                throw new DublicateUserException();
            }
            return enduser;
        }

        public User FindByEmail(string userEmail)
        {
            return uDB.FindByEmail(userEmail);
        }


        public User LogIn(string email, string clearTextPw)
        {
            User u = this.FindByEmail(email);
            if (u == null) return null;

            if (ValidatePassword(u, clearTextPw))
            {
                u.LogInSession = new Session();
                return u;
            }
            else
            {
                return null;
            }
        }

        public bool IsRegisteredToEvent(User u, Event e)
        {
            if (!this.IsValidated(u))
            {
                return false;
            }

            return u.Registrations.Select(reg => reg.Event).Contains(e);

        }

        public void AddRegistration(User user, Registration reg)
        {
            user.Registrations.Add(reg);
            uDB.Update(user);
        }

        public bool ValidatePassword(User u, string clearTextPw)
        {
            return HashingHelper.CheckPassword(clearTextPw, u.Salt, u.Password);
        }

        public bool IsValidated(User u1)
        {
            return SesCtrl.IsValidated(u1.LogInSession);

        }

        public User UpdateUserInfo(User u)
        {
            return uDB.FindByEmail(u.Email);
        }
    }
}
