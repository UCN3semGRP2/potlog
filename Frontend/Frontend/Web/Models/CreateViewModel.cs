using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class CreateViewModel
    {
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
        [Required(ErrorMessage = "Password skal udfyldes.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Gentag Password")]
        [Required(ErrorMessage = "Password og Gentag Password skal være identiske.")]
        [DataType(DataType.Password)]
        public string RepeatPassword { get; set; }
    }
}