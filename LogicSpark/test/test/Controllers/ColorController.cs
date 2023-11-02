
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
        private ApplicationDbContext _ColorController;

        public ColorController(ApplicationDbContext logger)
        {
            _ColorController = logger;
        }

        [HttpPost()]
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
        public IActionResult delete(int id)
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
        public IActionResult Update(int id, [FromBody] AD AdData)
        {
            var ADfromDb = _ColorController.ColorsSetting.FirstOrDefault(x => x.Id == id);
            if (ADfromDb != null)
            {
                ADfromDb.Dscrp = AdData.Dscrp;
                ADfromDb.Title = AdData.title;
                _ColorController.ColorsSetting.Update(ADfromDb);
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
