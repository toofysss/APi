using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace test
{
    [Table("websiteinfo")]
    public class Websiteinfo
    {
        [Key]
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Logoimg { get; set; }
        public ICollection<Color> ColorsSetting { get; set; }
    }
}
