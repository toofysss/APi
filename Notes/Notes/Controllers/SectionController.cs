using Microsoft.AspNetCore.Mvc;
using Notes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    [Controller]
    [Route("Section")]
    public class SectionController:ControllerBase
    {
        private ApplicationDbContext _sectionController;
        public SectionController(ApplicationDbContext logger) => _sectionController = logger;
        
        [HttpGet("GET")]
        public ActionResult<IEnumerable<Section>> GetSection() {
            return Ok(_sectionController.Sections); 
         }
   
        [HttpPost("Insert")]
        public ActionResult<IEnumerable<Section>> Post([FromBody] Section section)
        {
            _sectionController.Sections.Add(section);
            _sectionController.SaveChanges();
            return Ok("Succses");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<Section>>Update([FromBody] Section section)
        {
            var SectionID = _sectionController.Sections.FirstOrDefault(x => x.id == section.id);
            if (SectionID == null) return NotFound();
            SectionID.Sections = section.Sections;
            _sectionController.Sections.Update(SectionID);
            _sectionController.SaveChanges();
            return Ok("Succses");
        }

    }
}
