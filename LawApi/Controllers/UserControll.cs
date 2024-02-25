using LawApi.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;

namespace LawApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //////// المستخدمبن 

    public class UserControll: ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserControll(ApplicationDbContext logger)=>_context = logger;
   

        [HttpPost("AddUser")]
        public async Task<IActionResult> Add([FromBody] Users login)
        {
            if (login == null) return BadRequest();
            string passwordToHash = login.Password;
            var passwordHasher = new PasswordHasher<string>();
            var url = "https://api.ultramsg.com/instance70012/messages/chat";
            var client = new RestClient(url);
            string hashedPassword = passwordHasher.HashPassword(null, passwordToHash);
            var request = new RestRequest(url, Method.Post);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("token", "wpk7z4bt0epdwq4j");
            request.AddParameter("to",  login.Username);
            request.AddParameter("body", "كلمة المرور الخاصة بك :" + login.Password);
            await client.ExecuteAsync(request);
            login.Password = hashedPassword;
            _context.Users.Add(login);
            _context.SaveChanges();
          

            
            return Ok("Success");
        }

        [HttpGet("CheckLogin")]
        public IActionResult Login(string email, string pass)
        {
            if (email == null || pass == null)return BadRequest();
            var user = _context.Users.FirstOrDefault(log => log.Username == email);
            if (user == null)return NotFound("User not found");
            string storedHashedPassword = user.Password;
            var passwordHasher = new PasswordHasher<string>();
            var result = passwordHasher.VerifyHashedPassword(null, storedHashedPassword, pass);
            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)return Ok(user); 
            else return BadRequest("Incorrect password");
            
        }

        [HttpDelete("DelUser")]
        public IActionResult Delete(int LawersID)
        {
            var user = _context.Users.FirstOrDefault(log => log.LawersID == LawersID);
            if (user == null) return NotFound();
            _context.Remove(user);
            _context.SaveChanges();
            return Ok("Success");
        }
    }
}
