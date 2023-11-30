using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test.Database
{
    [Table("blogs")]
    public class Blogs
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Dscrp { get; set; }
        public string Img { get; set; }
        public DateTime Date { get; set; }
        public int TeamID { get; set; }
        public Team Team { get; set; }
    }
}
