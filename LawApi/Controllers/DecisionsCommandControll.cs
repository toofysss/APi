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
    //////// قرارات مجلس قيادة الثورة المنحل
    public class DecisionsCommandControll: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DecisionsCommandControll(ApplicationDbContext logger) => _context = logger;
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<DecisionsCommand>> GetNotes()
        {
            return Ok(_context.DecisionsCommand);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<DecisionsCommand>> Delete(int id)
        {
            var DecisionsCassationdata = _context.DecisionsCommand.FirstOrDefault(x => x.ID == id);
            if (DecisionsCassationdata == null) return NotFound();
            _context.DecisionsCommand.Remove(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<DecisionsCommand>> Insert([FromBody] DecisionsCommand Books)
        {
            _context.DecisionsCommand.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<DecisionsCommand>> Update([FromBody] DecisionsCommand Books)
        {
            var DecisionsCassationdata = _context.DecisionsCommand.FirstOrDefault(x => x.ID == Books.ID);
            if (DecisionsCassationdata == null) return NotFound();
          if(Books.Dscrp !=null)  DecisionsCassationdata.Dscrp = Books.Dscrp;
            _context.DecisionsCommand.Update(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }

}
