using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Notes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notes.Controllers
{
    [Controller]
    [Route("Notes")]
    public class DataNotesController:ControllerBase
    {
        private ApplicationDbContext _DataController;
        public DataNotesController(ApplicationDbContext logger) => _DataController = logger;
    
        [HttpGet]
        public ActionResult<IEnumerable<DataNotes>>GetNotes()
        {
            var sectionsWithDataNotes = _DataController.Sections.Include(x => x.DataNotes).ToList();
            var transformedData = sectionsWithDataNotes.Select(w => new
            {
                id = w.id,
                Section = w.Sections,
                Notes = w.DataNotes.Select(c => new
                {
                    id = c.id,
                    dscrp = c.Dscrp,
                    SectionID = c.SectionID,
                    bg = c.bg,
                }).ToList()
            });

            return Ok(transformedData);
        }
       
        [HttpPost("Insert")]
        public ActionResult<IEnumerable<DataNotes>> PostNotes([FromBody] DataNotes dataNotes)
        {
            if (dataNotes == null)return BadRequest("Invalid dataNotes"); 
            
            _DataController.DataNotes.Add(dataNotes);
            _DataController.SaveChanges();
            return Ok("Succses");
        }
      
        [HttpDelete("remove")]
        public ActionResult<object> remove([FromBody] DataNotes notes)
        {
            var SectionID = _DataController.DataNotes.FirstOrDefault(x => x.id == notes.id);
            if (SectionID == null) return NotFound();
            _DataController.DataNotes.Remove(SectionID);
            _DataController.SaveChanges();
            return Ok("Succses");
        }

    }
}
