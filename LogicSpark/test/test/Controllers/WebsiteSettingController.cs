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
    public class WebsiteSettingController :ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WebsiteSettingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class InsertDataDto
        {
            public Websiteinfo WebsiteInfo { get; set; }
            public List<Color> ColorsSetting { get; set; }
        }

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Websiteinfo>> GetAllWebsiteInfo()
        {
            var websiteInfo = _context.Websiteinfo.Include(w => w.ColorsSetting).ToList();
            var transformedData = websiteInfo.Select(w => new
            {
                id = w.Id,
                dscrp = w.Dscrp,
                logoimg = w.Logoimg,
                colorsSetting = w.ColorsSetting.Select(c => new
                {
                    id = c.Id,
                    dscrp = c.Dscrp,
                    title = c.Title,
                    websiteinfoID = c.WebsiteinfoID
                }).ToList()
            });

            return Ok(transformedData);
        }


        [HttpGet("GetByID")]
        public ActionResult<Websiteinfo> Get(int id)
        {
            var websiteInfo = _context.Websiteinfo
                .Include(w => w.ColorsSetting)
                .SingleOrDefault(w => w.Id == id);

            if (websiteInfo == null){
                return NotFound(); 
            }
            var transformedData = new
            {
                id = websiteInfo.Id,
                dscrp = websiteInfo.Dscrp,
                logoimg = websiteInfo.Logoimg,
                colorsSetting = websiteInfo.ColorsSetting.Select(c => new
                {
                    id = c.Id,
                    dscrp = c.Dscrp,
                    title = c.Title,
                    websiteinfoID = c.WebsiteinfoID
                }).ToList()
            };

            return Ok(transformedData);
        }
        
        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var websiteInfo = _context.Websiteinfo.Include(w => w.ColorsSetting).SingleOrDefault(w => w.Id == id);

            if (websiteInfo == null)
            {
                return NotFound(); 
            }
            _context.ColorsSetting.RemoveRange(websiteInfo.ColorsSetting);

            _context.Websiteinfo.Remove(websiteInfo);

            _context.SaveChanges();

            var transformedData = new
            {
                id = websiteInfo.Id,
                dscrp = websiteInfo.Dscrp,
                logoimg = websiteInfo.Logoimg,
                colorsSetting = websiteInfo.ColorsSetting.Select(c => new
                {
                    id = c.Id,
                    dscrp = c.Dscrp,
                    title = c.Title,
                    websiteinfoID = c.WebsiteinfoID
                }).ToList()
            };

            return Ok(transformedData);
        }
   

        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] InsertDataDto insertDataDto)
        {
            if (insertDataDto == null)
            {
                return BadRequest();
            }
            var websiteInfo = insertDataDto.WebsiteInfo;
            var colorsSetting = insertDataDto.ColorsSetting;
            if (websiteInfo == null || colorsSetting == null)
            {
                return BadRequest();
            }
            _context.Websiteinfo.Add(websiteInfo);
            _context.SaveChanges(); 
            foreach (var color in colorsSetting)
            {
                color.WebsiteinfoID = websiteInfo.Id;
                _context.ColorsSetting.Add(color);
            }
            _context.SaveChanges();

            var transformedData = new
            {
                id = websiteInfo.Id,
                dscrp = websiteInfo.Dscrp,
                logoimg = websiteInfo.Logoimg,
                colorsSetting = websiteInfo.ColorsSetting.Select(c => new
                {
                    id = c.Id,
                    dscrp = c.Dscrp,
                    title = c.Title,
                    websiteinfoID = c.WebsiteinfoID
                }).ToList()
            };

            return Ok(transformedData);
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromBody] InsertDataDto insertDataDto)
        {
            if (insertDataDto == null)
            {
                return BadRequest();
            }

            var websiteInfo = insertDataDto.WebsiteInfo;
            var colorsSetting = insertDataDto.ColorsSetting;

            if (websiteInfo == null || colorsSetting == null)
            {
                return BadRequest();
            }

            var existingWebsiteInfo = _context.Websiteinfo.Find(id);

            if (existingWebsiteInfo == null)
            {
                return NotFound();
            }
            existingWebsiteInfo.Dscrp = websiteInfo.Dscrp;
            existingWebsiteInfo.Logoimg = websiteInfo.Logoimg;
            var colorsToDelete = _context.ColorsSetting.Where(color => color.WebsiteinfoID == id);
            _context.ColorsSetting.RemoveRange(colorsToDelete);
            foreach (var color in colorsSetting)
            {
                color.WebsiteinfoID = id; 
                _context.ColorsSetting.Add(color);
            }

            _context.SaveChanges();

            var transformedData = new
            {
                id = websiteInfo.Id,
                dscrp = websiteInfo.Dscrp,
                logoimg = websiteInfo.Logoimg,
                colorsSetting = websiteInfo.ColorsSetting.Select(c => new
                {
                    id = c.Id,
                    dscrp = c.Dscrp,
                    title = c.Title,
                    websiteinfoID = c.WebsiteinfoID
                }).ToList()
            };

            return Ok(transformedData);
        }

    }
}
