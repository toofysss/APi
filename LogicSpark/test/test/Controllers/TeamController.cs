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

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Team>>> GetTeamDataAsync()
        {
            var teamData = await _TeamController.Team.Include(t => t.Social).Select(t => new
                {
                    id = t.Id,
                    name = t.Name,
                    title = t.Title,
                    job = t.Job,
                    img = t.Profileimg,
                    projects = _TeamController.Project.Where(p => p.Team.Id == t.Id).Select(p => p.Dscrp).ToList(),
                    social = _TeamController.Social.Where(p => p.Id == t.SocialID).ToList()
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
                    name = t.Name,
                    title = t.Title,
                    job = t.Job,
                    img = t.Profileimg,
                    projects = _TeamController.Project.Where(p => p.Team.Id == t.Id).Select(p => p.Dscrp).ToList(),
                    social = _TeamController.Social.Where(p => p.Id == t.SocialID).ToList()
                })
                .ToListAsync();

            return Ok(teamData);
        }

        [HttpPost("InsertData")]
        public ActionResult<IEnumerable<Team>> Insert([FromBody] Team Team)
        {
            if (Team == null)return BadRequest();

            var team = Team;
            var social = team.Social;
            team.Id= 0;
            _TeamController.Social.Add(social);
            _TeamController.SaveChanges();

            team.SocialID = social.Id;
            _TeamController.Team.Add(team);          
            _TeamController.SaveChanges();
            return Ok("Success");

        }

        [HttpPut("UpdateData")]
        public ActionResult<IEnumerable<Team>> Update([FromBody] Team UpdateDataDto)
        {
        var Teamid=_TeamController.Team.FirstOrDefault(x => x.Id == UpdateDataDto.Id);
          if(UpdateDataDto.Job !=null)  Teamid.Job = UpdateDataDto.Job;
            if (UpdateDataDto.Name != null) Teamid.Name = UpdateDataDto.Name;
            if (UpdateDataDto.Profileimg != null) Teamid.Profileimg = UpdateDataDto.Profileimg;
            if (UpdateDataDto.Title != null) Teamid.Title = UpdateDataDto.Title;
            if (UpdateDataDto == null|| Teamid==null) return BadRequest();
            var socialID = _TeamController.Social.FirstOrDefault(x => x.Id == UpdateDataDto.SocialID);
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
        public ActionResult<IEnumerable<Team>> Delete(int id)
        {
            var ADid = _TeamController.Team.Find(id);
            if (ADid != null)
            {
                var socialID = _TeamController.Social.FirstOrDefault(x => x.Id == ADid.SocialID);
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
