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
    //////// حقوقك

    public class RightGuideControll : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RightGuideControll(ApplicationDbContext logger) => _context = logger;

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<RightGuide>> GetNotes()
        {
            var sectionsWithDataNotes = _context.RightGuide.Include(x => x.RightDetails).ToList();
            var transformedData = sectionsWithDataNotes.Select(w => new
            {
                w.ID,
                w.Dscrp,
                Details = w.RightDetails.Select(c => new
                {
                    c.ID,
                    c.Qusetion,
                    c.Answer,
                    c.Laws,
                    c.RightGuideID
                }).ToList()
            });

            return Ok(transformedData);
        }
        [HttpDelete("DeleteHeader")]
        public ActionResult<IEnumerable<RightGuide>> DeleteHeader(int id)
        {
            var Booksdata = _context.RightGuide.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.RightGuide.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<RightDetails>> Delete(int id)
        {
            var Booksdata = _context.RightDetails.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.RightDetails.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("InsertHeader")]
        public ActionResult<IEnumerable<RightGuide>> InsertHeader([FromBody] RightGuide Books)
        {
            _context.RightGuide.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPost("Insert")]
        public ActionResult<IEnumerable<RightDetails>> Insert([FromBody] RightDetails Books)
        {
            _context.RightDetails.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("UpdateHeader")]
        public ActionResult<IEnumerable<RightGuide>> UpdateHeader([FromBody] RightGuide Books)
        {
            var Booksdata = _context.RightGuide.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
          if(Books.Dscrp !=null)  Booksdata.Dscrp = Books.Dscrp;
            _context.RightGuide.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<RightDetails>> Update([FromBody] RightDetails Books)
        {
            var Booksdata = _context.RightDetails.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Qusetion != null) Booksdata.Qusetion = Books.Qusetion;
            if (Books.Answer != null) Booksdata.Answer = Books.Answer;
            if (Books.Laws != null) Booksdata.Laws = Books.Laws;
            _context.RightDetails.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }
}
