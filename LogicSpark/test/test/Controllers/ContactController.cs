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
            if (!_ContactController.Contact.Any(ad => ad.Id == ContactData.Id))
            {
                ContactData.Id= 0;
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
        public IActionResult Delete(int id)
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
        public IActionResult Update( [FromBody] Contact ContactData)
        {
            var ContactfromDb = _ContactController.Contact.FirstOrDefault(x => x.Id == ContactData.Id);
            if (ContactfromDb != null)
            {
               if(ContactData.Instagram !=null) ContactfromDb.Instagram = ContactData.Instagram;
                if (ContactData.Telegram != null) ContactfromDb.Telegram = ContactData.Telegram;
                if (ContactData.Threads != null) ContactfromDb.Threads = ContactData.Threads;
                if (ContactData.Email != null) ContactfromDb.Email = ContactData.Email;
                if (ContactData.Linkedin != null) ContactfromDb.Linkedin = ContactData.Linkedin;
                if (ContactData.Location != null) ContactfromDb.Location = ContactData.Location;
                if (ContactData.PhoneNumber != null) ContactfromDb.PhoneNumber = ContactData.PhoneNumber;
                if (ContactData.Tiktok != null) ContactfromDb.Tiktok = ContactData.Tiktok;
                if (ContactData.Twitter != null) ContactfromDb.Twitter = ContactData.Twitter;
                if (ContactData.Whatsapp != null) ContactfromDb.Whatsapp = ContactData.Whatsapp;
                if (ContactData.Youtube != null) ContactfromDb.Youtube = ContactData.Youtube;
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
