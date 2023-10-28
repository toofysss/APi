using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    public class Team
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string job { get; set; }
        public string Profileimg { get; set; }
        public int SocialID { get; set; }

        public Social Social { get; set; }
    }

    public class Social
    {
        public int id { get; set; }
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

    [Table("projects")]
    public class projects
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public string ProjectFile { get; set; }
        public int TeamID { get; set; }
    }
}
