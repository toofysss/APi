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
    //////// قرارات مجلس الوزراء

    public class DecisionsCouncilControll: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DecisionsCouncilControll(ApplicationDbContext logger) => _context = logger;
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<DecisionsCouncil>> GetNotes()
        {
            return Ok(_context.DecisionsCouncil);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<DecisionsCouncil>> Delete(int id)
        {
            var DecisionsCassationdata = _context.DecisionsCouncil.FirstOrDefault(x => x.ID == id);
            if (DecisionsCassationdata == null) return NotFound();
            _context.DecisionsCouncil.Remove(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<DecisionsCouncil>> Insert([FromBody] DecisionsCouncil Books)
        {
            _context.DecisionsCouncil.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<DecisionsCouncil>> Update([FromBody] DecisionsCouncil Books)
        {
            var DecisionsCassationdata = _context.DecisionsCouncil.FirstOrDefault(x => x.ID == Books.ID);
            if (DecisionsCassationdata == null) return NotFound();
            if (Books.Dscrp != null) DecisionsCassationdata.Dscrp = Books.Dscrp;
            _context.DecisionsCouncil.Update(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }
}
