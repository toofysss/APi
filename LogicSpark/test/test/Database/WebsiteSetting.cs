using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace test
{
    public class Websiteinfo
    {
        [Key]
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Logoimg { get; set; }

        [JsonIgnore]
        public List<Color> ColorsSetting { get; set; }
    }
}
