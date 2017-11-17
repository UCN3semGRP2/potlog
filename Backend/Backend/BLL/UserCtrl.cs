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

        private UserDB uDB = new UserDB();

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
            uDB.Commit();
            return enduser;
        }

        public User LogIn(string email, string clearTextPw)
        {
            User u = uDB.FindByEmail(email);
            if (u == null) return null;

            if (ValidatePassword(u, clearTextPw))
            {
                u.LogInSession = new Session();
                return u;
            } else
            {
                return null;
            }
        }

        public void AddRegistration(User user, Registration reg)
        {
            user.Registrations.Add(reg);
            // uDB.Update(user); if entity isnt smart add this line
        }

        public bool ValidatePassword(User u, string clearTextPw)
        {
            return HashingHelper.CheckPassword(clearTextPw, u.Salt, u.Password);
        }

        public bool IsValidated(User u1)
        {
            return SesCtrl.IsValidated(u1.LogInSession);
            
        }
    }
}
