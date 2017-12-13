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
            if (!Validator.ValidateFirstname(Firstname))
            {
                throw new ArgumentException("Der skal indtastes fornavn");
            }
            if (!Validator.ValidateLastname(Lastname))
            {
                throw new ArgumentException("Der skal indtastes efternavn");
            }
            if (!Validator.ValidateEmail(Email))
            {
                throw new ArgumentException("Der skal indtastes en gyldig email");
            }
            
            if (!Validator.ValidatePassword(Password))
            {
                throw new ArgumentException(string.Format("Der skal indtastes et password på mindst {} karakterer", Validator.MinPasswordLength));
            }

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

            return uDB.IsRegisteredToEvent(u, e);

        }

        public void AddRegistration(User user, Registration reg)
        {
            if (user.Registrations == null)
            {
                user.Registrations = new List<Registration>();
            }
            user.Registrations.Add(reg);
            uDB.Update(user);
        }

        public bool ValidatePassword(User u, string clearTextPw)
        {
            return HashingHelper.CheckPassword(clearTextPw, u.Salt, u.Password);
        }

        public bool IsValidated(User u1)
        {
            return true; // TODO: hotfix for demonstration.
            return SesCtrl.IsValidated(u1.LogInSession);
        }

        public User UpdateUserInfo(User u)
        {
            return uDB.FindByEmail(u.Email);
        }

        public Event AcceptInviteString(User usr, string inviteString)
        {
            // TODO check that the user is logged in
            var eCtrl = new EventCtrl();
            Event e = eCtrl.FindByInviteString(inviteString);
            if (e == null) return null;

            eCtrl.RegisterToEvent(e, usr);
            return e;
        }
    }
}
