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
    ////////الكتب السنوية
    public class BooksControll : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BooksControll(ApplicationDbContext logger) => _context = logger;
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Books>> GetNotes()
        {
            return Ok(_context.Books);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<Books>> Delete(int id)
        {
            var Booksdata = _context.Books.FirstOrDefault(x => x.ID == id);
            if (Booksdata == null) return NotFound();
            _context.Books.Remove(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<Books>> Insert([FromBody] Books Books)
        {
            _context.Books.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<Books>> Update([FromBody] Books Books)
        {
            var Booksdata = _context.Books.FirstOrDefault(x => x.ID == Books.ID);
            if (Booksdata == null) return NotFound();
           if(Books.Dscrp !=null) Booksdata.Dscrp = Books.Dscrp;
            if (Books.Files != null) Booksdata.Files = Books.Files;
            _context.Books.Update(Booksdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }
}
