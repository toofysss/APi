using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdController:ControllerBase
    {
        private readonly ILogger<AdController> _adController;

        public AdController(ILogger<AdController> logger)
        {
            _adController = logger;
        }

        [HttpGet]
        public IEnumerable<AD> Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new AD
            {
                Id = index,
                title = "saad",
                Dscrp = "hi my name is mustafa " + index,
                Link = "the link is" + index,
                img = "The Img " + index,
                bg = "The bg is " + index,

            })
            .ToArray();

            return result;
        }
    }
}
