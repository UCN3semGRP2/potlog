using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Desktop
{
    
    public static class ValidateHelper
    {
        static Regex regexNumbers = new Regex("^[0-9]+$");
        
        public static bool checkStringForNumbers(string testString)
        {
            if(!regexNumbers.IsMatch(testString))
            {
                return false;
            }

            int val;
            Int32.TryParse(testString, out val);
            return val > 0;
        }

        public static void checkNumberTooltip(TextBox tb)
        {
            if (checkStringForNumbers(tb.Text))
            {
                tb.ToolTip = null;
                tb.BorderBrush = Brushes.Black;

            }
            else
            {
                tb.BorderBrush = Brushes.Red;
                tb.ToolTip = "Der må kun skrives tal større end 0 i dette felt";
            }
        }

        public static bool validatePassword(string s)
        {
            return (s.Length >= 6);
        }

        public static bool validateRepeatPassword(string password, string repeatPassword)
        {
            return password.Equals(repeatPassword);
        }

        public static bool isEntered(TextBox tb)
        {
            return (tb.Text.Length != 0);
        }

        public static bool validateEmail(string s)
        {
            try
            {
                new MailAddress(s);
                return true;
            }
            catch (Exception e)
            {
                if (e is FormatException || e is ArgumentException || e is ArgumentNullException)
                {
                    return false;
                }
                throw e;
            }
        }
            
    }
}
