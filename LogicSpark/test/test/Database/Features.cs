using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace test
{
    [Table("features")]

    public class Features
    {
        public int Id { get; set; }
        public string  Dscrp { get; set; }
        public string Img { get; set; }

    }
}
