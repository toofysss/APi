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
    //////// البنك المركزي العراقي

    public class IraqBankController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IraqBankController(ApplicationDbContext logger) => _context = logger;

        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<IraqBank>> GetNotes()
        {
            var sectionsWithDataNotes = _context.IraqBank.Include(x => x.IraqBankDetails).ToList();
            var transformedData = sectionsWithDataNotes.Select(w => new
            {
                w.ID,
                w.Dscrp,
                Details = w.IraqBankDetails.Select(c => new
                {
                    c.ID,
                    c.Title,
                    c.Dscrp,
                    c.Files,
                    c.Img,
                    c.IraqBankID
                }).ToList()
            });

            return Ok(transformedData);
        }

        [HttpDelete("DeleteHeader")]
        public ActionResult<IEnumerable<IraqBank>> DeleteHeader(int id)
        {
            var Booksdata = _context.IraqBank.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.IraqBank.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<IraqBankDetails>> Delete(int id)
        {
            var Booksdata = _context.IraqBankDetails.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.IraqBankDetails.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("InsertHeader")]
        public ActionResult<IEnumerable<IraqBank>> InsertHeader([FromBody] IraqBank Books)
        {
            _context.IraqBank.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPost("Insert")]
        public ActionResult<IEnumerable<IraqBankDetails>> Insert([FromBody] IraqBankDetails Books)
        {
            _context.IraqBankDetails.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("UpdateHeader")]
        public ActionResult<IEnumerable<IraqBank>> UpdateHeader([FromBody] IraqBank Books)
        {
            var Booksdata = _context.IraqBank.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Dscrp != null) Booksdata.Dscrp = Books.Dscrp;
            _context.IraqBank.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<IraqBankDetails>> Update([FromBody] IraqBankDetails Books)
        {
            var Booksdata = _context.IraqBankDetails.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
            if (Books.Dscrp != null) Booksdata.Dscrp = Books.Dscrp;
            if (Books.Files != null) Booksdata.Files = Books.Files;
            if (Books.Title != null) Booksdata.Title = Books.Title;
            if (Books.Img != null) Booksdata.Img = Books.Img;
            _context.IraqBankDetails.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }

}
