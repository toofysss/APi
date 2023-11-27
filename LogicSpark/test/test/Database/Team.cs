using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    [Table("team")]
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Job { get; set; }
        public string Profileimg { get; set; }
        public int SocialID { get; set; }

        public Social Social { get; set; }
    }
    [Table("social")]
    public class Social
    {
        public int Id { get; set; }
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
    public class Projects
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Details { get; set; }
        public string Link { get; set; }
        public string ProjectFile { get; set; }
        public int TeamID { get; set; }
    }
}
