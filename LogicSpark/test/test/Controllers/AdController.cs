using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Database;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdController:ControllerBase
    {
        private ApplicationDbContext _adController;

        public AdController(ApplicationDbContext logger)
        {
            _adController = logger;
        }

        [HttpGet("GetAll")]
        public IEnumerable<AD> Get()
        {
            return _adController.ad;
        }

        [HttpGet("GetByID")]
        public ActionResult<AD> Get(int id)
        {
            var getbyid = _adController.ad.Where(Features => Features.Id == id);
            if (getbyid != null)
            {
                return Ok(getbyid);
            }
            else
            {
                return NotFound();
            };
        }

        [HttpPost("Insert")]
        public IActionResult Post([FromBody] AD AdData)
        {
            if (!_adController.ad.Any(ad => ad.Id == AdData.Id))
            {
                _adController.ad.Add(AdData);
                _adController.SaveChanges();
                return Ok(_adController.ad);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("Delete")]
        public IActionResult delete(int id)
        {
            var ADid = _adController.ad.Find(id);
            if (ADid !=null)
            {
                _adController.ad.Remove(ADid);
                _adController.SaveChanges();
                return Ok(_adController.ad);
            }
            else
            {
                return NotFound();
            }
        }
     
        [HttpPut("Update")]
        public IActionResult Update(int id,[FromBody] AD AdData) 
        {
            var ADfromDb = _adController.ad.FirstOrDefault(x=>x.Id ==id);
            if (ADfromDb != null)
            {
                ADfromDb.img = AdData.img;
                ADfromDb.Link = AdData.Link;
                ADfromDb.title = AdData.title;
                _adController.ad.Update(ADfromDb);
                _adController.SaveChanges();
                return Ok(_adController.ad);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
