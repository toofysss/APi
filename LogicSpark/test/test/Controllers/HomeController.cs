﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using test.Database;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<HomeData>> GetData()
        {
            try
            {
                var adData = await _context.Ad.ToListAsync();
                var featuresData = await _context.Features.ToListAsync();
                var teamData = await _context.Team.Include(t => t.Social).Select(t => new
                {
                    id = t.Id,
                    name = t.Name,
                    title = t.Title,
                    job = t.Job,
                    img = t.Profileimg,
                    projects = _context.Project.Where(p => p.Team.Id == t.Id).ToList(),
                    social = _context.Social.Where(p => p.Id == t.SocialID).ToList()
                }).ToListAsync();
                var projectData =  await _context.Project.Include(p => p.ProjectImg).Select(p => new
                {
                    p.Id,
                    p.Dscrp,
                    p.Details,
                    p.Link,
                    p.ProjectFile,
                    ProjectImg = _context.ProjectImg.Where(pi => pi.ProjectId == p.Id).ToList(),
                    Team = _context.Team
                    .Where(t => t.Id == p.TeamID)
                    .Include(t => t.Social)
                    .Where(t => t.Social.Id == t.SocialID)
                    .ToList()   
                }).ToListAsync();
                var contactData = await _context.Contact.ToListAsync();
                var websiteInfoData = await _context.Websiteinfo.Include(w => w.ColorsSetting).ToListAsync();

                var response = new 
                {
                    Ad = adData,
                    Features = featuresData,
                    Team = teamData,
                    Project = projectData,
                    contact = contactData,
                    WebsiteSetting = websiteInfoData
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred: " + ex.Message);
            }
        }

    }
}
