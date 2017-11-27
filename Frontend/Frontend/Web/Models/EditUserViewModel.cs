using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class EditUserViewModel
    {
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public int id { get; set; }
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Email skal udfyldes.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Display(Name = "Fornavn")]
        [Required(ErrorMessage = "Fornavn skal udfyldes.")]
        public string Firstname { get; set; }
        [Display(Name = "Efternavn")]
        [Required(ErrorMessage = "Efternavn skal udfyldes.")]
        public string Lastname { get; set; }
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Gentag Password")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
    }
}