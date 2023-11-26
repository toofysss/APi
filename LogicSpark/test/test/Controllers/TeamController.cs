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
    public class TeamController:ControllerBase
    {
        private readonly ApplicationDbContext _TeamController;
        public TeamController(ApplicationDbContext team) => _TeamController = team;


        public class InsertDataTeam
        {
            public Team Team { get; set; }
            public Social Social { get; set; }
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamDataAsync()
        {
            var teamData = await _TeamController.Team.Include(t => t.Social).Select(t => new
                {
                    id = t.Id,
                    name = t.name,
                    title = t.title,
                    job = t.job,
                    img = t.Profileimg,
                    projects = _TeamController.Project.Where(p => p.Team.Id == t.Id).Select(p => p.Dscrp).ToList(),
                    social = _TeamController.Social.Where(p => p.id == t.SocialID).ToList()
                }).ToListAsync();

            return Ok(teamData);
        }




        [HttpGet("GetByID")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeam(int id)
        {
            var teamData = await _TeamController.Team
                .Include(t => t.Social)
                .Where(t => t.Id == id)
                .Select(t => new
                {
                    id = t.Id,
                    name = t.name,
                    title = t.title,
                    job = t.job,
                    img = t.Profileimg,
                    projects = _TeamController.Project.Where(p => p.Team.Id == t.Id).Select(p => p.Dscrp).ToList(),
                    social = _TeamController.Social.Where(p => p.id == t.SocialID).ToList()
                })
                .ToListAsync();

            return Ok(teamData);
        }

        [HttpPost("InsertData")]
        public async Task<ActionResult<IEnumerable<Team>>> Insert([FromBody] InsertDataTeam insertDataDto)
        {
            if (insertDataDto == null)
            {
                return BadRequest();
            }
            var team = insertDataDto.Team;
            var social = insertDataDto.Social;

            if (team == null || social == null)
            {
                return BadRequest();
            }
            team.Id= 0;
            _TeamController.Social.Add(social);
            _TeamController.SaveChanges();

            team.SocialID = social.id;
                _TeamController.Team.Add(team);
            
            _TeamController.SaveChanges();


            return Ok("Success");

        }

        [HttpPut("UpdateData")]
        public async Task<ActionResult<IEnumerable<Team>>> Update([FromBody] Team UpdateDataDto)
        {
        var Teamid=_TeamController.Team.FirstOrDefault(x => x.Id == UpdateDataDto.Id);
          if(UpdateDataDto.job !=null)  Teamid.job = UpdateDataDto.job;
            if (UpdateDataDto.name != null) Teamid.name = UpdateDataDto.name;
            if (UpdateDataDto.Profileimg != null) Teamid.Profileimg = UpdateDataDto.Profileimg;
            if (UpdateDataDto.title != null) Teamid.title = UpdateDataDto.title;
            if (UpdateDataDto == null|| Teamid==null) return BadRequest();
            var socialID = _TeamController.Social.FirstOrDefault(x => x.id == UpdateDataDto.SocialID);
            if (UpdateDataDto.Social.Instagram != null) socialID.Instagram = UpdateDataDto.Social.Instagram;
            if (UpdateDataDto.Social.Linkedin != null) socialID.Linkedin = UpdateDataDto.Social.Linkedin;
            if (UpdateDataDto.Social.Telegram != null) socialID.Telegram = UpdateDataDto.Social.Telegram;
            if (UpdateDataDto.Social.Threads != null) socialID.Threads = UpdateDataDto.Social.Threads;
            if (UpdateDataDto.Social.Tiktok != null) socialID.Tiktok = UpdateDataDto.Social.Tiktok;
            if (UpdateDataDto.Social.Twitter != null) socialID.Twitter = UpdateDataDto.Social.Twitter;
            if (UpdateDataDto.Social.Whatsapp != null) socialID.Whatsapp = UpdateDataDto.Social.Whatsapp;
            if (UpdateDataDto.Social.Youtube != null) socialID.Youtube = UpdateDataDto.Social.Youtube;

            _TeamController.Social.Update(socialID);
            _TeamController.Team.Update(Teamid);
            _TeamController.SaveChanges();
            return Ok(GetTeamDataAsync());

        }

        [HttpDelete("DeleteData")]
        public async Task<ActionResult<IEnumerable<Team>>> Delete(int id)
        {
            var ADid = _TeamController.Team.Find(id);
            if (ADid != null)
            {
                var socialID = _TeamController.Social.FirstOrDefault(x => x.id == ADid.SocialID);
                _TeamController.Social.Remove(socialID);
                _TeamController.Team.Remove(ADid);
                _TeamController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
