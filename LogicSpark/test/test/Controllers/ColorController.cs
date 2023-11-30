
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using test.Database;

namespace test.Database
{
    [ApiController]
    [Route("[controller]")]
    public class ColorController : ControllerBase
    {
        private readonly ApplicationDbContext _ColorController;

        public ColorController(ApplicationDbContext logger)
        {
            _ColorController = logger;
        }

        [HttpPost("Insert")]
        public ActionResult Post([FromBody] IEnumerable<Color> AdDataCollection)
        {
            foreach (var adData in AdDataCollection)
            {
                var lastid = _ColorController.Websiteinfo
                    .OrderByDescending(w => w.Id)
                    .FirstOrDefault();

                if (lastid != null)
                {
                    adData.WebsiteinfoId = lastid.Id;
                }
              
                _ColorController.ColorsSetting.Add(adData);
            }

            _ColorController.SaveChanges();
            return Ok("Success");
        }






        [HttpDelete("DELETE")]
        public IActionResult Delete(int id)
        {
            var ADid = _ColorController.ColorsSetting.Find(id);
            if (ADid != null)
            {
                _ColorController.ColorsSetting.Remove(ADid);
                _ColorController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("UPDATE")]
        public IActionResult Update([FromBody] Color color)
        {
            var AdDataCollection = _ColorController.ColorsSetting.FirstOrDefault(x => x.Id == color.Id);
            if (AdDataCollection != null)
            {
               if(color.Dscrp !=null) AdDataCollection.Dscrp = color.Dscrp;
               if (color.Title != null) AdDataCollection.Title = color.Title;
               if (color.WebsiteinfoId >0) AdDataCollection.WebsiteinfoId = color.WebsiteinfoId;
                _ColorController.ColorsSetting.Update(AdDataCollection);
                _ColorController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

    }
    }
