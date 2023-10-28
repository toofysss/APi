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
    [Controller]
    [Route("controller")]
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

        //[HttpGet("lastid")]
        //public ActionResult<int> GetLastWebsiteinfoId()
        //{
        //    var lastWebsiteinfo = _context.Websiteinfo
        //        .OrderByDescending(w => w.Id)
        //        .FirstOrDefault();

        //    if (lastWebsiteinfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return lastWebsiteinfo.Id;
        //}


        //public class InsertDataDto
        //{
        //    public Websiteinfo WebsiteInfo { get; set; }
        //    public List<Color> ColorsSetting { get; set; }
        //}

        [HttpPost("Insert")]
        public IActionResult Post([FromBody] Websiteinfo WebsiteinfoData)
        {
            if (!_context.Websiteinfo.Any(ad => ad.Id == WebsiteinfoData.Id))
            {
                WebsiteinfoData.ColorsSetting = null;
                _context.Websiteinfo.Add(WebsiteinfoData);
                _context.SaveChanges();
                return Ok(_context.Websiteinfo);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("Delete")]
        public IActionResult delete(int id)
        {
            var ADid = _context.Websiteinfo.Find(id);
            if (ADid != null)
            {
                _context.Websiteinfo.Remove(ADid);
                _context.SaveChanges();
                return Ok(_context.ad);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromBody] Websiteinfo AdData)
        {
            var ADfromDb = _context.Websiteinfo.FirstOrDefault(x => x.Id == id);
            if (ADfromDb != null)
            {
                ADfromDb.Dscrp = AdData.Dscrp;
                ADfromDb.Logoimg = AdData.Logoimg;
                ADfromDb.ColorsSetting = null;
                _context.Websiteinfo.Update(ADfromDb);
                _context.SaveChanges();
                return Ok(_context.Websiteinfo);
            }
            else
            {
                return NotFound();
            }
        }
      
        //[HttpGet("GetByID")]
            //public ActionResult<Websiteinfo> Get(int id)
            //{
            //    var websiteInfo = _context.Websiteinfo
            //        .Include(w => w.ColorsSetting)
            //        .SingleOrDefault(w => w.Id == id);

            //    if (websiteInfo == null)
            //    {
            //        return NotFound();
            //    }
            //    var transformedData = new
            //    {
            //        id = websiteInfo.Id,
            //        dscrp = websiteInfo.Dscrp,
            //        logoimg = websiteInfo.Logoimg,
            //        colorsSetting = websiteInfo.ColorsSetting.Select(c => new
            //        {
            //            id = c.Id,
            //            dscrp = c.Dscrp,
            //            title = c.Title,
            //            websiteinfoID = c.WebsiteinfoID
            //        }).ToList()
            //    };

            //    return Ok(transformedData);
            //}

            //[HttpDelete("Delete")]
            //public IActionResult Delete(int id)
            //{
            //    var websiteInfo = _context.Websiteinfo.Include(w => w.ColorsSetting).SingleOrDefault(w => w.Id == id);

            //    if (websiteInfo == null)
            //    {
            //        return NotFound();
            //    }
            //    _context.ColorsSetting.RemoveRange(websiteInfo.ColorsSetting);

            //    _context.Websiteinfo.Remove(websiteInfo);

            //    _context.SaveChanges();

            //    var transformedData = new
            //    {
            //        id = websiteInfo.Id,
            //        dscrp = websiteInfo.Dscrp,
            //        logoimg = websiteInfo.Logoimg,
            //        colorsSetting = websiteInfo.ColorsSetting.Select(c => new
            //        {
            //            id = c.Id,
            //            dscrp = c.Dscrp,
            //            title = c.Title,
            //            websiteinfoID = c.WebsiteinfoID
            //        }).ToList()
            //    };

            //    return Ok(transformedData);
            //}


            //[HttpPost("Insert")]
            //public IActionResult Insert([FromBody] InsertDataDto insertDataDto)
            //{
            //    if (insertDataDto == null)
            //    {
            //        return BadRequest();
            //    }
            //    var websiteInfo = insertDataDto.WebsiteInfo;
            //    var colorsSetting = insertDataDto.ColorsSetting;
            //    if (websiteInfo == null || colorsSetting == null)
            //    {
            //        return BadRequest();
            //    }
            //    _context.Websiteinfo.Add(websiteInfo);
            //    _context.SaveChanges();
            //    foreach (var color in colorsSetting)
            //    {
            //        color.WebsiteinfoID = websiteInfo.Id;
            //        _context.ColorsSetting.Add(color);
            //    }
            //    _context.SaveChanges();

            //    var transformedData = new
            //    {
            //        id = websiteInfo.Id,
            //        dscrp = websiteInfo.Dscrp,
            //        logoimg = websiteInfo.Logoimg,
            //        colorsSetting = websiteInfo.ColorsSetting.Select(c => new
            //        {
            //            id = c.Id,
            //            dscrp = c.Dscrp,
            //            title = c.Title,
            //            websiteinfoID = c.WebsiteinfoID
            //        }).ToList()
            //    };

            //    return Ok(transformedData);
            //}

            //[HttpPut("Update")]
            //public IActionResult Update(int id, [FromBody] InsertDataDto insertDataDto)
            //{
            //    if (insertDataDto == null)
            //    {
            //        return BadRequest();
            //    }

            //    var websiteInfo = insertDataDto.WebsiteInfo;
            //    var colorsSetting = insertDataDto.ColorsSetting;

            //    if (websiteInfo == null || colorsSetting == null)
            //    {
            //        return BadRequest();
            //    }

            //    var existingWebsiteInfo = _context.Websiteinfo.Find(id);

            //    if (existingWebsiteInfo == null)
            //    {
            //        return NotFound();
            //    }
            //    existingWebsiteInfo.Dscrp = websiteInfo.Dscrp;
            //    existingWebsiteInfo.Logoimg = websiteInfo.Logoimg;
            //    var colorsToDelete = _context.ColorsSetting.Where(color => color.WebsiteinfoID == id);
            //    _context.ColorsSetting.RemoveRange(colorsToDelete);
            //    foreach (var color in colorsSetting)
            //    {
            //        color.WebsiteinfoID = id;
            //        _context.ColorsSetting.Add(color);
            //    }

            //    _context.SaveChanges();

            //    var transformedData = new
            //    {
            //        id = websiteInfo.Id,
            //        dscrp = websiteInfo.Dscrp,
            //        logoimg = websiteInfo.Logoimg,
            //        colorsSetting = websiteInfo.ColorsSetting.Select(c => new
            //        {
            //            id = c.Id,
            //            dscrp = c.Dscrp,
            //            title = c.Title,
            //            websiteinfoID = c.WebsiteinfoID
            //        }).ToList()
            //    };

            //    return Ok(transformedData);
            //}

        }
}
