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
                    t.Id,
                    t.Name,
                    t.Title,
                    t.Job,
                    t.Profileimg,
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
                    t.Id,
                    t.Name,
                    t.Title,
                    t.Job,
                    t.Profileimg,
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
            team.SocialID = social.Id;
            _TeamController.Social.Add(social);
            _TeamController.SaveChanges();
            _TeamController.Team.Add(team);          
            _TeamController.SaveChanges();
            return Ok("Success");

        }

        [HttpPut("UpdateData")]
        public ActionResult<IEnumerable<Team>> Update([FromBody] Team Team)
        {
        var Teamid=_TeamController.Team.FirstOrDefault(x => x.Id == Team.Id);
            if (Teamid == null) return NotFound();
            if (Team.Name != null) Teamid.Name = Team.Name;            
            if (Team.Title != null) Teamid.Title = Team.Title;
            if(Team.Job !=null)  Teamid.Job = Team.Job;
            if (Team.Profileimg != null) Teamid.Profileimg = Team.Profileimg;
            if (Team.SocialID >0) Teamid.SocialID = Team.SocialID;
            var socialID = _TeamController.Social.FirstOrDefault(x => x.Id == Teamid.SocialID);
            if (socialID != null)
            {
            if (Team.Social.Instagram != null) socialID.Instagram = Team.Social.Instagram;
            if (Team.Social.Linkedin != null) socialID.Linkedin = Team.Social.Linkedin;
            if (Team.Social.Telegram != null) socialID.Telegram = Team.Social.Telegram;
            if (Team.Social.Facebook != null) socialID.Facebook = Team.Social.Facebook;
            if (Team.Social.Threads != null) socialID.Threads = Team.Social.Threads;
            if (Team.Social.Tiktok != null) socialID.Tiktok = Team.Social.Tiktok;
            if (Team.Social.Twitter != null) socialID.Twitter = Team.Social.Twitter;
            if (Team.Social.Whatsapp != null) socialID.Whatsapp = Team.Social.Whatsapp;
            if (Team.Social.Youtube != null) socialID.Youtube = Team.Social.Youtube;
            _TeamController.Social.Update(socialID);
            }


            _TeamController.Team.Update(Teamid);
            _TeamController.SaveChanges();
            return Ok("success");

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
