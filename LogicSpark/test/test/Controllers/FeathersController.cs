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
    public class FeathersController : ControllerBase
    {
        private readonly ILogger<FeathersController> _FeathersController;

        public FeathersController(ILogger<FeathersController> logger)
        {
            _FeathersController = logger;
        }

        [HttpGet]
        public IEnumerable<Features> Get()
        {
            var rng = new Random();
            var result = Enumerable.Range(1, 5).Select(index => new Features
            {
                Id = index,
                Img = "saad",
                Dscrp = "hi my name is mustafa " + index
            })
            .ToArray(); 

            return result;
        }
    }
}
