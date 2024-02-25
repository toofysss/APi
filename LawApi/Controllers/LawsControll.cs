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
    //////// القوانين

    public class LawsControll:ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LawsControll(ApplicationDbContext logger) => _context = logger;

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Law>> GetNotes()
        {
            var sectionsWithDataNotes = _context.Law.Include(x => x.LawDetails).ToList();
            var transformedData = sectionsWithDataNotes.Select(w => new
            {
                w.ID,
                w.Dscrp,
                Details = w.LawDetails.Select(c => new
                {
                    c.ID,
                    c.Dscrp,
                    c.LawID
                }).ToList()
            });

            return Ok(transformedData);
        }
        [HttpDelete("DeleteHeader")]
        public ActionResult<IEnumerable<Law>> DeleteHeader(int id)
        {
            var Booksdata = _context.Law.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.Law.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<LawDetails>> Delete(int id)
        {
            var Booksdata = _context.LawDetails.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.LawDetails.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("InsertHeader")]
        public ActionResult<IEnumerable<Law>> InsertHeader([FromBody] Law Books)
        {
            _context.Law.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPost("Insert")]
        public ActionResult<IEnumerable<LawDetails>> Insert([FromBody] LawDetails Books)
        {
            _context.LawDetails.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("UpdateHeader")]
        public ActionResult<IEnumerable<Law>> UpdateHeader([FromBody] Law Books)
        {
            var Booksdata = _context.Law.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if(Books.Dscrp !=null)   Booksdata.Dscrp = Books.Dscrp;
            _context.Law.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<LawDetails>> Update([FromBody] LawDetails Books)
        {
            var Booksdata = _context.LawDetails.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Dscrp != null) Booksdata.Dscrp = Books.Dscrp;
            _context.LawDetails.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }
}
