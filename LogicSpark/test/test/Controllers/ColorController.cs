
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
        public IActionResult Update( [FromBody] AD AdData)
        {
            var ADfromDb = _ColorController.ColorsSetting.FirstOrDefault(x => x.Id == AdData.Id);
            if (ADfromDb != null)
            {
               if(AdData.Dscrp !=null) ADfromDb.Dscrp = AdData.Dscrp;
                if (AdData.Title != null) ADfromDb.Title = AdData.Title;
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
