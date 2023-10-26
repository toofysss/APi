using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test
{
    public class Color
    {
        public int Id { get; set; }
        public string Dscrp { get; set; }

        public string Title { get; set; }
        public int WebsiteSettingId { get; set; }
        public WebsiteSetting WebsiteSetting { get; set; }
    }
}
