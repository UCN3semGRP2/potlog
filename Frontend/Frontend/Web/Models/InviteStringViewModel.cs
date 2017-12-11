using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class InviteStringViewModel
    {
        [Display(Name = "Invitationskode")]
        [Required(ErrorMessage = "Invitationskode skal udfyldes.")]
        [DataType(DataType.Text)]
        public string InviteString { get; set; }
    }
}