using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Threading.Tasks;
using test.Database;

namespace test.Controllers
{
    [Controller]
    [Route("controller")]
    public class TeamController:ControllerBase
    {
        private readonly ApplicationDbContext _TeamController;
        public TeamController(ApplicationDbContext team) => _TeamController = team;


        public class InsertDataTeam
        {

            public Team Team { get; set; }

            public Social Social { get; set; }
        }
        [HttpGet("Team/GetAll")]
        public async Task<IEnumerable<object>> GetTeamDataAsync()
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

            return teamData;
        }




        [HttpGet("Team/GetByID")]
        public async Task<IEnumerable<object>> GetTeam(int id)
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

            return teamData;
        }

        [HttpPost("Team/InsertData")]
        public async Task<ActionResult<IEnumerable<object>>> Insert([FromBody] InsertDataTeam insertDataDto)
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

            _TeamController.Social.Add(social);
            _TeamController.SaveChanges();

            team.SocialID = social.id;
                _TeamController.Team.Add(team);
            
            _TeamController.SaveChanges();


            return Ok(GetTeamDataAsync());

        }

        [HttpPut("Team/UpdateData")]
        public async Task<ActionResult<IEnumerable<object>>> Update(int id, [FromBody] Team UpdateDataDto)
        {
        var Teamid=_TeamController.Team.FirstOrDefault(x => x.Id == id);
            Teamid.job = UpdateDataDto.job;
            Teamid.name = UpdateDataDto.name;
            Teamid.Profileimg = UpdateDataDto.Profileimg;
            Teamid.title = UpdateDataDto.title;
            if (UpdateDataDto == null|| Teamid==null) return BadRequest();
            var socialID = _TeamController.Social.FirstOrDefault(x => x.id == UpdateDataDto.SocialID);
            socialID.Instagram = UpdateDataDto.Social.Instagram;
            socialID.Linkedin = UpdateDataDto.Social.Linkedin;
            socialID.Telegram = UpdateDataDto.Social.Telegram;
            socialID.Threads = UpdateDataDto.Social.Threads;
            socialID.Tiktok = UpdateDataDto.Social.Tiktok;
            socialID.Twitter = UpdateDataDto.Social.Twitter;
            socialID.Whatsapp = UpdateDataDto.Social.Whatsapp;
            socialID.Youtube = UpdateDataDto.Social.Youtube;

            _TeamController.Social.Update(socialID);
            _TeamController.Team.Update(Teamid);
            _TeamController.SaveChanges();
            return Ok(GetTeamDataAsync());

        }

        [HttpDelete("Team/DeleteData")]
        public async Task<ActionResult<IEnumerable<object>>> Delete(int id)
        {
            var ADid = _TeamController.Team.Find(id);
            if (ADid != null)
            {
                var socialID = _TeamController.Social.FirstOrDefault(x => x.id == ADid.SocialID);
                _TeamController.Social.Remove(socialID);
                _TeamController.Team.Remove(ADid);
                _TeamController.SaveChanges();
                return GetTeamDataAsync();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
