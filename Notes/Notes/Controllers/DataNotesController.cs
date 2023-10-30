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
      
        [HttpPut("Update")]
        public ActionResult<object> UpdateNotes(int id, [FromBody] DataNotes notes)
        {
            if (notes == null)return BadRequest("Bad Request");

            var existingData = _DataController.DataNotes.FirstOrDefault(x => x.id == id);
            if (existingData == null)return NotFound("Not Found");
            
            if (notes.Dscrp != null)existingData.Dscrp = notes.Dscrp;
            
            if (notes.bg != null)existingData.bg = notes.bg;
            

            _DataController.DataNotes.Update(existingData);
            _DataController.SaveChanges();
            return Ok("Succses");
        }

    }
}
