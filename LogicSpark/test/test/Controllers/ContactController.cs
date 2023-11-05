using Microsoft.AspNetCore.Mvc;
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
    public class ContactController : ControllerBase
    {
        private readonly ApplicationDbContext _ContactController;

        public ContactController(ApplicationDbContext logger)
        {
            _ContactController = logger;
        }

        [HttpGet("GetAll")]
        public ActionResult<Contact> Get()
        {
            return Ok(_ContactController.Contact);
        }

        [HttpPost("Insert")]
        public IActionResult Post([FromBody] Contact ContactData)
        {
            if (!_ContactController.Contact.Any(ad => ad.id == ContactData.id))
            {
                _ContactController.Contact.Add(ContactData);
                _ContactController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("Delete")]
        public IActionResult delete(int id)
        {
            var ContactDataid = _ContactController.Contact.Find(id);
            if (ContactDataid != null)
            {
                _ContactController.Contact.Remove(ContactDataid);
                _ContactController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(int id, [FromBody] Contact ContactData)
        {
            var ContactfromDb = _ContactController.Contact.FirstOrDefault(x => x.id == id);
            if (ContactfromDb != null)
            {
                ContactfromDb.Instagram = ContactData.Instagram;
                ContactfromDb.Telegram = ContactData.Telegram;
                ContactfromDb.Threads = ContactData.Threads;
                ContactfromDb.Email = ContactData.Email;
                ContactfromDb.Linkedin = ContactData.Linkedin;
                ContactfromDb.Location = ContactData.Location;
                ContactfromDb.phoneNumber = ContactData.phoneNumber;
                ContactfromDb.Tiktok = ContactData.Tiktok;
                ContactfromDb.Twitter = ContactData.Twitter;
                ContactfromDb.Whatsapp = ContactData.Whatsapp;
                ContactfromDb.Youtube = ContactData.Youtube;
                _ContactController.Contact.Update(ContactfromDb);
                _ContactController.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }

    }
}
