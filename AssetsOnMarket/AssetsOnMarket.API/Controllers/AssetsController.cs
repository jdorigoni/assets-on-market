using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AssetsOnMarket.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AssetsController : ControllerBase
    {
        private readonly ILogger<AssetsController> _logger;

        public AssetsController(ILogger<AssetsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public string HealthCheck()
        {
            return "API up and running";
        }
    }
}
