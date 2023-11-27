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
        private readonly ApplicationDbContext _context;

        public BlogController(ApplicationDbContext logger) => _context = logger;



        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Blogs>>> GetTeamDataAsync()
        {
            var teamData = await _context.Blogs.Include(t => t.Team).Select(p => new
            {
                p.Id,
                p.Dscrp,
                p.Title,
                p.Img,
                p.Date,
                team= _context.Team
                    .Where(t => t.Id == p.TeamID)
                    .Include(t => t.Social)
                    .Where(t => t.Social.Id == t.SocialID)
                    .ToList()
            }).ToListAsync();

            return Ok(teamData);
        }
        [HttpGet("GetByID")]
        public async  Task<ActionResult<IEnumerable<Blogs>>> GetTeam(int id)
        {
            var teamData = await _context.Blogs
                .Include(t => t.Team)
                .Where(t => t.Id == id)
                .Select(p => new
                {
                    p.Id,
                    p.Dscrp,
                    p.Title,
                    p.Img,
                    team = _context.Team
                    .Where(t => t.Id == p.TeamID)
                    .Include(t => t.Social)
                    .Where(t => t.Social.Id == t.SocialID)
                    .ToList()
                }).ToListAsync();

            return Ok(teamData);
        }

        [HttpPost("InsertData")]
        public ActionResult<IEnumerable<Blogs>> Insert([FromBody] Blogs blogs)
        {
            if (!_context.Blogs.Any(ad => ad.Id == blogs.Id))
            {
                blogs.Id= 0;
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
        public IActionResult Delete(int id)
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
        public IActionResult Update( [FromBody] Blogs blogs)
        {
            var ADfromDb = _context.Blogs.FirstOrDefault(x => x.Id == blogs.Id);
            if (ADfromDb == null) NotFound();
            if (blogs.Title != null) ADfromDb.Title = blogs.Title;
            if (blogs.Dscrp != null) ADfromDb.Dscrp = blogs.Dscrp;
            if (blogs.Img !=null)   ADfromDb.Img = blogs.Img;
            if (blogs.Date != null) ADfromDb.Date = blogs.Date;
            if (blogs.TeamID >0) ADfromDb.TeamID = blogs.TeamID;

                _context.Blogs.Update(ADfromDb);
                _context.SaveChanges();
                return Ok("Success");

        }
    }
   
}
