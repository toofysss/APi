using LawApi.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LawApi.Controllers
{

    [ApiController]
    [Route("[controller]")]
    ////////   قرارات محكمة التمييز

    public class DecisionsCourtCassationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DecisionsCourtCassationController(ApplicationDbContext logger) => _context = logger;

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<DecisionsCourtCassation>> GetNotes()
        {
            var sectionsWithDataNotes = _context.DecisionsCourtCassation.Include(x => x.DecisionsCourtCassationDetails).ToList();
            var transformedData = sectionsWithDataNotes.Select(w => new
            {
                w.ID,
                w.Dscrp,
                Details = w.DecisionsCourtCassationDetails.Select(c => new
                {
                    c.ID,
                    c.Title,
                    c.Dscrp,
                    c.Files,
                    c.Img,
                    c.DecisionsCourtCassationID

                }).ToList()
            });

            return Ok(transformedData);
        }

        [HttpDelete("DeleteHeader")]
        public ActionResult<IEnumerable<DecisionsCourtCassation>> DeleteHeader(int id)
        {
            var Booksdata = _context.DecisionsCourtCassation.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.DecisionsCourtCassation.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<DecisionsCourtCassationDetails>> Delete(int id)
        {
            var Booksdata = _context.DecisionsCourtCassationDetails.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.DecisionsCourtCassationDetails.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("InsertHeader")]
        public ActionResult<IEnumerable<DecisionsCourtCassation>> InsertHeader([FromBody] DecisionsCourtCassation Books)
        {
            _context.DecisionsCourtCassation.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPost("Insert")]
        public ActionResult<IEnumerable<DecisionsCourtCassationDetails>> Insert([FromBody] DecisionsCourtCassationDetails Books)
        {
            _context.DecisionsCourtCassationDetails.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("UpdateHeader")]
        public ActionResult<IEnumerable<DecisionsCourtCassation>> UpdateHeader([FromBody] DecisionsCourtCassation Books)
        {
            var Booksdata = _context.DecisionsCourtCassation.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Dscrp != null) Booksdata.Dscrp = Books.Dscrp;
            _context.DecisionsCourtCassation.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<DecisionsCourtCassationDetails>> Update([FromBody] DecisionsCourtCassationDetails Books)
        {
            var Booksdata = _context.DecisionsCourtCassationDetails.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            Booksdata.Dscrp = Books.Dscrp;
            Booksdata.Files = Books.Files;
            Booksdata.Title = Books.Title;
            Booksdata.Img = Books.Img;
            _context.DecisionsCourtCassationDetails.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }

}
