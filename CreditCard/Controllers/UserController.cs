using CreditCard.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Notes.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCard.Controllers
{
    public class UserController:ControllerBase
    {
        private readonly ApplicationDb _DataController;
        public UserController(ApplicationDb logger) => _DataController = logger;
        [HttpPost("AddUser")]
        public IActionResult Add([FromBody] User login)
        {
            if (login == null) return BadRequest();
            string passwordToHash = login.password;
            var passwordHasher = new PasswordHasher<string>();
            string hashedPassword = passwordHasher.HashPassword(null, passwordToHash);
            login.password = hashedPassword;
            _DataController.User.Add(login);
            _DataController.SaveChanges();
            return Ok("Success");
        }
        [HttpPost("CheckLogin")]
        public IActionResult Login([FromBody] User login)
        {    
                if (login == null) return BadRequest();
                var user = _DataController.User.FirstOrDefault(log => log.Email == login.Email);
                if (user == null) return NotFound();
                string storedHashedPassword = user.password;
                var passwordHasher = new PasswordHasher<string>();
                var result = passwordHasher.VerifyHashedPassword(null, storedHashedPassword, login.password);
                if (result == Microsoft.AspNetCore.Identity.PasswordVerificationResult.Success)return Ok(user);               
                else return Ok("Faield");              
        }

    }
}
