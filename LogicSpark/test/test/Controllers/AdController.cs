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
    public class AdController : ControllerBase
    {
        private readonly ApplicationDbContext _adController;

        public AdController(ApplicationDbContext logger)
        {
            _adController = logger;
        }

        [HttpGet("GetAll")]
        public IEnumerable<AD> Get()
        {
            return _adController.Ad;
        }

        [HttpGet("GetByID")]
        public ActionResult<AD> Get(int id)
        {
            var getbyid = _adController.Ad.Where(Features => Features.Id == id);
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
            if (!_adController.Ad.Any(ad => ad.Id == AdData.Id))
            {
                AdData.Id= 0;
                _adController.Ad.Add(AdData);
                _adController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var ADid = _adController.Ad.Find(id);
            if (ADid != null)
            {
                _adController.Ad.Remove(ADid);
                _adController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update( [FromBody] AD AdData)
        {
            var ADfromDb = _adController.Ad.FirstOrDefault(x => x.Id == AdData.Id);
            if (ADfromDb != null)
            {
               if(AdData.Img !=null) ADfromDb.Img = AdData.Img;
                if (AdData.Link != null) ADfromDb.Link = AdData.Link;
                if (AdData.Img2 != null) ADfromDb.Img2 = AdData.Img2;
                if (AdData.Title != null) ADfromDb.Title = AdData.Title;
                _adController.Ad.Update(ADfromDb);
                _adController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }
  
    }
}
