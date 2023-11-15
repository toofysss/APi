using CreditCard.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Notes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCard.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ADController:ControllerBase
    {
        private readonly ApplicationDb _DataController;
        public ADController(ApplicationDb logger) => _DataController = logger;
        [HttpGet("AD")]
        public async Task<ActionResult<IEnumerable<Ads>>> GetData(int count)
        {
            var lastThreeAds = await _DataController.AD
                .OrderByDescending(ad => ad.Id)
                .Take(count)
                .ToListAsync();
            return Ok(lastThreeAds);
        }

        [HttpPost("Update")]
        public IActionResult InsertDate([FromForm] Ads AD)
        {
            _DataController.AD.Add(AD);
            _DataController.SaveChanges();
            return Ok("Sucess");
        }

        [HttpPut("ADD")]
        public IActionResult UpdateDate([FromForm] Ads AD)
        {
            var AdData = _DataController.AD.FirstOrDefault(x => x.Id == AD.Id);
            if (AdData == null) return NotFound();
            AdData.Img = AD.Img;
            AdData.Dscrp = AD.Dscrp;
            _DataController.AD.Update(AdData);
            _DataController.SaveChanges();
            return Ok("Sucess");
        }
        [HttpDelete("Remove")]
        public IActionResult RemoveDate([FromForm] Ads AD)
        {
            var AdData = _DataController.AD.FirstOrDefault(x => x.Id == AD.Id);
            if (AdData == null) return NotFound();
            _DataController.AD.Remove(AdData);
            _DataController.SaveChanges();
            return Ok("Sucess");
        }
    }
}
