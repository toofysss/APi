using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Controllers
{
    [Controller]
    [Route("controller")]
    public class WebsiteSettingController :ControllerBase
    {
        private static readonly List<WebsiteSetting> websitedata = new List<WebsiteSetting>
        {
            new WebsiteSetting {Id = 1, Dscrp = "Setting 1",Logoimg="saa", ColorsSetting = new List<Color>() {
                    new Color { Id = 1, Dscrp = "Color 1", Title = "Red" },
                    new Color { Id = 1, Dscrp = "Color 2", Title = "Blue" },
                    new Color { Id = 1, Dscrp = "Color 1", Title = "Red" },
                    new Color { Id = 1, Dscrp = "Color 2", Title = "Blue" }
            }
            },
            new WebsiteSetting { Id = 2, Dscrp = "Setting 2"},
            new WebsiteSetting { Id = 3, Dscrp = "Setting 3" }
        };

        [HttpGet]
        public IEnumerable<WebsiteSetting> Get()
        {

            var result = websitedata
           .ToArray();

            return result;
        }
    }
       
    }
