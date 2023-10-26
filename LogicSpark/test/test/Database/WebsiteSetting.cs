using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test
{
    public class WebsiteSetting
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }
        public string Logoimg { get; set; }

        public ICollection<Color> ColorsSetting { get; set; }
    }
}
