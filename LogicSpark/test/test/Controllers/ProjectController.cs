using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Database;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<object>>> GetWebsiteInfos()
        {
            var projects = await _context.Project.Include(p => p.ProjectImg).Select(p => new
            {
                Id = p.Id,
                Dscrp = p.Dscrp,
                Details = p.Details,
                Link = p.Link,
                ProjectFile = p.ProjectFile,
                ProjectImg = _context.ProjectImg.Where(pi => pi.ProjectId == p.Id).ToList(),
                Team = _context.Team
                .Where(t => t.Id == p.TeamID)
                .Include(t => t.Social)
                .Where(t => t.Social.id == t.SocialID)
                .ToList()
            }).ToListAsync();
            return projects;
        }

        [HttpGet("GetByID")]
        public async Task<IEnumerable<object>> GetTeam(int id)
        {
            var Project = await _context.Project
                .Include(t => t.ProjectImg)
                .Where(t => t.Id == id).
                 Select(p => new
                 {
                     Id = p.Id,
                     Dscrp = p.Dscrp,
                     Details = p.Details,
                     Link = p.Link,
                     ProjectFile = p.ProjectFile,
                     ProjectImg = _context.ProjectImg.Where(pi => pi.ProjectId == p.Id).ToList(),
                     Team = _context.Team
                     .Where(t => t.Id == p.TeamID)
                     .Include(t => t.Social)
                     .Where(t => t.Social.id == t.SocialID)
                     .ToList()
                 }).ToListAsync();

            return Project;
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(int id)
        {
            var projectsToDelete = _context.Project.Find(id);

            if (projectsToDelete != null)
            {
                var projectImages = _context.ProjectImg.Where(pi => pi.ProjectId == projectsToDelete.Id).ToList();

                _context.ProjectImg.RemoveRange(projectImages);
                _context.Project.Remove(projectsToDelete);
                _context.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }
        public class InsertDataDto
        {
            public Project project { get; set; }

            public List<ProjectImg> Img { get; set; }
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromBody] Project project)
        {
            var projectFromDb = _context.Project.FirstOrDefault(x => x.Id == id);

            if (projectFromDb == null) return NotFound();

            projectFromDb.ProjectFile = project.ProjectFile;
            projectFromDb.Link = project.Link;
            projectFromDb.TeamID =project.TeamID;
            projectFromDb.Dscrp = project.Dscrp;
            projectFromDb.Details = project.Details;
            foreach (var projectImg in project.ProjectImg)
            {
                var check = _context.ProjectImg.FirstOrDefault(pi => pi.Id == projectImg.Id && pi.ProjectId ==id);
                if (check != null)
                {
                    check.ProjectId = project.Id;
                    check.Dscrp = projectImg.Dscrp;
                    _context.ProjectImg.Update(check);
                }
                else
                {
                    var newProjectImg = new ProjectImg
                    {
                        Id =0, 
                        ProjectId = id, 
                        Dscrp = projectImg.Dscrp 
                    };
                    _context.ProjectImg.Add(newProjectImg);
                }    
            }
            _context.Project.Update(projectFromDb);
            _context.SaveChanges();
            return Ok("Success");
        }




        [HttpPost("Insert")]
        public IActionResult Insert([FromBody] Project project)
        {
            if (project == null)
                return BadRequest();

            _context.Project.Add(project);

            foreach (var projectImg in project.ProjectImg)
            {
                projectImg.ProjectId = project.Id;
                _context.ProjectImg.Add(projectImg);
            }

            _context.SaveChanges();

            return Ok("Success");
        }

    }
}

