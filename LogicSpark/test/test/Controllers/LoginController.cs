using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using test.Database;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("AddUser")]
        public IActionResult Add([FromBody] Login login)
        {
            if (login == null)return BadRequest();
            string passwordToHash = login.Pass;
            var passwordHasher = new PasswordHasher<string>();
            string hashedPassword = passwordHasher.HashPassword(null, passwordToHash);
            login.Pass = hashedPassword;
            _context.Login.Add(login);
            _context.SaveChanges();
            return Ok("Success");
        }
        [HttpGet("CheckLogin")]
        public IActionResult Login(string email ,string pass)
        {

            if (email == null) return BadRequest();
            var user = _context.Login.FirstOrDefault(log=> log.Username == email);

            string storedHashedPassword = user.Pass;

            var passwordHasher = new PasswordHasher<string>();
            var result = passwordHasher.VerifyHashedPassword(null, storedHashedPassword, pass);

            if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)
            {
                return Ok(user); 
            }else
            {
                return Ok("Faield");
            }
        }
    }
}
