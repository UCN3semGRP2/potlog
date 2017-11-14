using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class UserLogInViewModel
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email skal udfyldes.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password skal udfyldes.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}