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
    //////// قرارات المحكمة الاتحادية العليا
    public class DecisionsFederalSupremeCourtController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DecisionsFederalSupremeCourtController(ApplicationDbContext logger) => _context = logger;
        [HttpGet("{cound}/{Date}")]
        public ActionResult<IEnumerable<DecisionsFederalSupremeCourt>> GetNotes(string cound , string Date)
        {
            return Ok(_context.DecisionsFederalSupremeCourt.FirstOrDefault(x => x.Count == cound && x.Date ==Date));
        }
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<DecisionsFederalSupremeCourt>> GetAll()
        {
            return Ok(_context.DecisionsFederalSupremeCourt);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<DecisionsFederalSupremeCourt>> Delete(int id)
        {
            var Booksdata = _context.DecisionsFederalSupremeCourt.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.DecisionsFederalSupremeCourt.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<DecisionsFederalSupremeCourt>> Insert([FromBody] DecisionsFederalSupremeCourt Books)
        {
            _context.DecisionsFederalSupremeCourt.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<DecisionsFederalSupremeCourt>> Update([FromBody] DecisionsFederalSupremeCourt Books)
        {
            var Booksdata = _context.DecisionsFederalSupremeCourt.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Count != null) Booksdata.Count = Books.Count;
            if (Books.Date != null) Booksdata.Date = Books.Date;
            if (Books.Files != null) Booksdata.Files = Books.Files;
            _context.DecisionsFederalSupremeCourt.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
    }

}
