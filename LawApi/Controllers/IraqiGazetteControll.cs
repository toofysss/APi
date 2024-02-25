using LawApi.Database;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    //////// جريدة العراق
    public class IraqiGazetteControll : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IraqiGazetteControll(ApplicationDbContext logger) => _context = logger;
        [HttpGet("{cound}")]
        public ActionResult<IEnumerable<IraqiGazette>> GetNotes(string cound)
        {
            return Ok(_context.IraqiGazette.FirstOrDefault(x=>x.Count==cound));
        }
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<IraqiGazette>> GetNotes()
        {
            return Ok(_context.IraqiGazette);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<IraqiGazette>> Delete(int id)
        {
            var Booksdata = _context.IraqiGazette.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.IraqiGazette.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<IraqiGazette>> Insert([FromBody] IraqiGazette Books)
        {
            _context.IraqiGazette.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<IraqiGazette>> Update([FromBody] IraqiGazette Books)
        {
            var Booksdata = _context.IraqiGazette.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Count != null) Booksdata.Count = Books.Count;
            if (Books.Files != null) Booksdata.Files = Books.Files;
            _context.IraqiGazette.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
    }
}
