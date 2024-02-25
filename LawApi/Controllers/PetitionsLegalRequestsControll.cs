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
    ////////  العرائض والطلبات

    public class PetitionsLegalRequestsControll : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetitionsLegalRequestsControll(ApplicationDbContext logger) => _context = logger;
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<PetitionsLegalRequests>> GetNotes()
        {
            return Ok(_context.PetitionsLegalRequests);
        }
        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<PetitionsLegalRequests>> Delete(int id)
        {
            var DecisionsCassationdata = _context.PetitionsLegalRequests.FirstOrDefault(x => x.ID == id);
            if (DecisionsCassationdata == null) return NotFound();
            _context.PetitionsLegalRequests.Remove(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<PetitionsLegalRequests>> Insert([FromBody] PetitionsLegalRequests Books)
        {
            _context.PetitionsLegalRequests.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<PetitionsLegalRequests>> Update([FromBody] PetitionsLegalRequests Books)
        {
            var DecisionsCassationdata = _context.PetitionsLegalRequests.FirstOrDefault(x => x.ID == Books.ID);
            if (DecisionsCassationdata == null) return NotFound();
            if (Books.Dscrp != null) DecisionsCassationdata.Dscrp = Books.Dscrp;
            if (Books.Details != null) DecisionsCassationdata.Details = Books.Details;
            _context.PetitionsLegalRequests.Update(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }

}
