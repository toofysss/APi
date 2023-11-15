using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCard.Database
{
    [Table("T_User")]
    public class User
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public string password { get; set; }
        public string Phone { get; set; }
    }
}
