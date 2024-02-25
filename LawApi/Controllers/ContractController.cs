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
    //////// العقود 

    public class ContractController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ContractController(ApplicationDbContext logger) => _context = logger;
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<Contract>> GetNotes()
        {
            return Ok(_context.Contract);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<Contract>> Delete(int id)
        {
            var DecisionsCassationdata = _context.Contract.FirstOrDefault(x => x.ID == id);
            if (DecisionsCassationdata == null) return NotFound();
            _context.Contract.Remove(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<Contract>> Insert([FromBody] Contract Books)
        {
            _context.Contract.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<Contract>> Update([FromBody] Contract Books)
        {
            var DecisionsCassationdata = _context.Contract.FirstOrDefault(x => x.ID == Books.ID);
            if (DecisionsCassationdata == null) return NotFound();
          if(Books.Dscrp !=null)  DecisionsCassationdata.Dscrp = Books.Dscrp;
            if (Books.Details != null) DecisionsCassationdata.Details = Books.Details;
            _context.Contract.Update(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }

}
