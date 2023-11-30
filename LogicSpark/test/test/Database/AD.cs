using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test
{
    [Table("ad")]
    public class AD
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Dscrp { get; set; }
        public string Link { get; set; }
        public string Img { get; set; }
        public string Bg { get; set; }
    }
}
