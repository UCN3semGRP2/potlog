using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [Index("Email_Index", IsUnique = true)]
        [StringLength(255)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }

        public Session LogInSession { get; set; }
    }
}
