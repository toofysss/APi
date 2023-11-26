using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using test.Database;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeathersController : ControllerBase
    {
        private ApplicationDbContext _FeathersController;

        public FeathersController(ApplicationDbContext logger)
        {
            _FeathersController = logger;
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Features>> Get()
        {
            return Ok(_FeathersController.Features);
        }

        [HttpGet("GetByID")]
        public ActionResult<IEnumerable<Features>> Get(int id)
        {
            var getbyid = _FeathersController.Features.Where(Features => Features.Id == id);
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
        public IActionResult Post([FromBody] Features FeaturesData)
        {
            if (!_FeathersController.Features.Any(Features => Features.Id == FeaturesData.Id))
            {
                _FeathersController.Features.Add(FeaturesData);
                _FeathersController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("Delete")]
        public IActionResult delete(int id)
        {
            var Featuresid = _FeathersController.Features.Find(id);
            if (Featuresid != null)
            {
                _FeathersController.Features.Remove(Featuresid);
                _FeathersController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromBody] Features FeaturesData)
        {
            var FeaturesfromDb = _FeathersController.Features.FirstOrDefault(x => x.Id == id);
            if (FeaturesfromDb != null)
            {
                FeaturesfromDb.Img = FeaturesData.Img;
                FeaturesfromDb.Dscrp = FeaturesData.Dscrp;
                _FeathersController.Features.Update(FeaturesfromDb);
                _FeathersController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
