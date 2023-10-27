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
       };


        [HttpGet("GetAll")]
        public IEnumerable<HomeClass> Get()
        {

            return Homedata;
        }
    }
}
