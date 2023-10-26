using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class ContactController :ControllerBase
    {
        private readonly ILogger<ContactController> _ContactController;

        public ContactController(ILogger<ContactController> logger)
        {
            _ContactController = logger;
        }

        [HttpGet]
        public IEnumerable<Contact> Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new Contact
            {
                Id = index,
                Email = "my email is " + index,
                Instagram = "my Instagram is" + index,
                Telegram = "my telegram is" + index,
                Facebook ="my Facebook is "+index,
                Threads ="my Threads is "+index,
                Linkedin ="my linkedin is " +index,
                Location ="my location is "+index,
                Phone ="my Phone is 964"+index,
                Tiktok="my Tiktok is "+index,
                Twitter ="my Twitter is" +index,
                Whatsapp="my whatsapp is" +index,
                Youtube="my youtube is "+index
            })
            .ToArray();

            return result;
        }
    }
}
