using System;
using System.Net.Mail;

namespace BLL
{
    internal class Validator
    {
        static public int MinPasswordLength = 6;

        internal static bool ValidateFirstname(string firstname)
        {
            return firstname.Length > 1;
        }

        internal static bool ValidateLastname(string lastname)
        {
            return lastname.Length > 1;
        }

        internal static bool ValidateEmail(string email)
        {
            try
            {
                _ = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        internal static bool ValidatePassword(string password)
        {
            return password.Length >= 6;
        }
    }
}