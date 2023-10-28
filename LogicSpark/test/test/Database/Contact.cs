using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using test.Database;

namespace test
{
    [Table("contact")]
    public class Contact
    {
        public int id { get; set; }
        public string Email { get; set; }
        public string Location { get; set;}
        public string phoneNumber { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Youtube { get; set; }
        public string Facebook { get; set; }
        public string Tiktok { get; set; }
        public string Whatsapp { get; set; }
        public string Twitter { get; set; }
        public string Threads { get; set; }
        public string Linkedin { get; set; }

    }
}
