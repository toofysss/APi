using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    [Table("login")]
    public class Login
    {
        public int id { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }

    }
}
