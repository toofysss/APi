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
        public async Task<ActionResult<IEnumerable<Project>>> GetWebsiteInfos()
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
            return Ok(projects);
        }

        [HttpGet("GetByID")]
        public async Task<ActionResult<IEnumerable<Project>>> GetTeam(int id)
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

            return Ok(Project);
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
        public IActionResult Update( [FromBody] Project project)
        {
            var projectFromDb = _context.Project.FirstOrDefault(x => x.Id == project.Id);

            if (projectFromDb == null) return NotFound();

            if(project.ProjectFile !=null)   projectFromDb.ProjectFile = project.ProjectFile;
            if (project.Link != null) projectFromDb.Link = project.Link;
            if (project.TeamID != null) projectFromDb.TeamID =project.TeamID;
            if (project.Dscrp != null) projectFromDb.Dscrp = project.Dscrp;
            if (project.Details != null) projectFromDb.Details = project.Details;
            foreach (var projectImg in project.ProjectImg)
            {
                var check = _context.ProjectImg.FirstOrDefault(pi => pi.Id == projectImg.Id && pi.ProjectId == project.Id);
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
                        ProjectId = project.Id, 
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
            project.Id= 0;
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

