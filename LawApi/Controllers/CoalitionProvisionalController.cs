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
    //////// اوامر سلطة الائتلاف المؤقتة
    public class CoalitionProvisionalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CoalitionProvisionalController(ApplicationDbContext logger) => _context = logger;
        [HttpGet("GetAll")]
        public ActionResult<IEnumerable<CoalitionProvisional>> GetNotes()
        {
            return Ok(_context.CoalitionProvisional);
        }

        [HttpDelete("Delete")]
        public ActionResult<IEnumerable<CoalitionProvisional>> Delete(int id)
        {
            var DecisionsCassationdata = _context.CoalitionProvisional.FirstOrDefault(x => x.ID == id);
            if (DecisionsCassationdata == null) return NotFound();
            _context.CoalitionProvisional.Remove(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

        [HttpPost("Insert")]
        public ActionResult<IEnumerable<CoalitionProvisional>> Insert([FromBody] CoalitionProvisional Books)
        {
            _context.CoalitionProvisional.Add(Books);
            _context.SaveChanges();
            return Ok("success");
        }
        [HttpPut("Update")]
        public ActionResult<IEnumerable<CoalitionProvisional>> Update([FromBody] CoalitionProvisional Books)
        {
            var DecisionsCassationdata = _context.CoalitionProvisional.FirstOrDefault(x => x.ID == Books.ID);
            if (DecisionsCassationdata == null) return NotFound();
           if(Books.Dscrp !=null) DecisionsCassationdata.Dscrp = Books.Dscrp;
            _context.CoalitionProvisional.Update(DecisionsCassationdata);
            _context.SaveChanges();
            return Ok("success");
        }

    }

}
