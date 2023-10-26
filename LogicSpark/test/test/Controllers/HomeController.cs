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
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _HomeController;

        public HomeController(ILogger<HomeController> logger)
        {
            _HomeController = logger;
        }
        private static readonly List<HomeClass> Homedata = new List<HomeClass>
        {
                new HomeClass
                {
                    WebsiteSetting =new List<WebsiteSetting>{
                        new WebsiteSetting
                        {
                            Dscrp = "Setting 2"
                        }, },
                    contact = new List<Contact>
                        {
                            new Contact
                            {
                                Tiktok = "mustafa saad ticktock"
                            },
                            new Contact
                            {
                                Tiktok = "mustafa saad2 ticktock"
                            }
                        },
                    Ad = new List<AD>
                    {
                        new AD
                        
                        {
                            title = "mustafa saad ad title"
                        } },
                    team = new List<Team>
                    { new Team
                        {
                            Job = "mustafa saad is Team Leader"
                        }}
                }
        };


        [HttpGet("GetAll")]
        public IEnumerable<HomeClass> Get()
        {

            return Homedata;
        }
    }
}
