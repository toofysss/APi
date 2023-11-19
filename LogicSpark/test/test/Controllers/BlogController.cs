using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Database;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private ApplicationDbContext _context;

        public BlogController(ApplicationDbContext logger) => _context = logger;



        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Blogs>>> GetTeamDataAsync()
        {
            var teamData = await _context.Blogs.Include(t => t.Team).Select(p => new
            {
                id = p.id,
                Dscrp = p.Dscrp,
                title = p.title,
                img = p.img,
                team= _context.Team
                    .Where(t => t.Id == p.TeamID)
                    .Include(t => t.Social)
                    .Where(t => t.Social.id == t.SocialID)
                    .ToList()
            }).ToListAsync();

            return Ok(teamData);
        }
        [HttpGet("GetByID")]
        public async  Task<ActionResult<IEnumerable<Blogs>>> GetTeam(int id)
        {
            var teamData = await _context.Blogs
                .Include(t => t.Team)
                .Where(t => t.id == id)
                .Select(p => new
                {
                    id = p.id,
                    Dscrp = p.Dscrp,
                    title = p.title,
                    img = p.img,
                    team = _context.Team
                    .Where(t => t.Id == p.TeamID)
                    .Include(t => t.Social)
                    .Where(t => t.Social.id == t.SocialID)
                    .ToList()
                }).ToListAsync();

            return Ok(teamData);
        }

        [HttpPost("InsertData")]
        public async Task<ActionResult<IEnumerable<Blogs>>> Insert([FromBody] Blogs blogs)
        {
            if (!_context.Blogs.Any(ad => ad.id == blogs.id))
            {
                _context.Blogs.Add(blogs);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }

        }
        [HttpDelete("Delete")]
        public IActionResult delete(int id)
        {
            var ADid = _context.Blogs.Find(id);
            if (ADid != null)
            {
                _context.Blogs.Remove(ADid);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromBody] Blogs blogs)
        {
            var ADfromDb = _context.Blogs.FirstOrDefault(x => x.id == id);
            if (ADfromDb != null)
            {
                ADfromDb.img = blogs.img;
                ADfromDb.Dscrp = blogs.Dscrp;
                ADfromDb.title = blogs.title;
                ADfromDb.TeamID = blogs.TeamID;

                _context.Blogs.Update(ADfromDb);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }
    }
   
}
