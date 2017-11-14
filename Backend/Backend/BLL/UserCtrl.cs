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

        public void CreateUser(string Firstname, string Lastname, string Email, string Password)
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

            using (DALContext db = new DALContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public User LogIn(string email, string clearTextPw)
        {
            using (DALContext db = new DALContext())
            {
                User u = db.Users.First(x => x.Email == email);
                if (ValidatePassword(u, clearTextPw))
                {
                    u.LogInSession = new Session();
                    return u;
                }
                return null;
            }
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
