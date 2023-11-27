using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public class WebsiteSettingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WebsiteSettingController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Websiteinfo>>> GetWebsiteInfos()
        {
            return await  _context.Websiteinfo.Include(wi => wi.ColorsSetting).ToListAsync();
        }

        [HttpPost("Insert")]
        public IActionResult Post([FromBody] Websiteinfo WebsiteinfoData)
        {
            if (!_context.Websiteinfo.Any(ad => ad.Id == WebsiteinfoData.Id))
            {
                WebsiteinfoData.ColorsSetting = WebsiteinfoData.ColorsSetting;
                WebsiteinfoData.Id = 0;
                _context.Websiteinfo.Add(WebsiteinfoData);
                _context.SaveChanges();
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
            var ADid = _context.Websiteinfo.Find(id);
            if (ADid != null)
            {
                _context.Websiteinfo.Remove(ADid);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update( [FromBody] Websiteinfo AdData)
        {
            var ADfromDb = _context.Websiteinfo.FirstOrDefault(x => x.Id == AdData.Id);
            if (ADfromDb == null) return NotFound();
            
               if(AdData.Dscrp !=null) ADfromDb.Dscrp = AdData.Dscrp;
               if (AdData.Logoimg != null) ADfromDb.Logoimg = AdData.Logoimg;
               if (AdData.ColorsSetting != null) ADfromDb.ColorsSetting = null;
               _context.Websiteinfo.Update(ADfromDb);
               _context.SaveChanges();
               return Ok("Success");
           
        }
      
    }
}
