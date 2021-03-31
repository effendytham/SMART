using Backend.Module.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        public SeedController(IElasticSeed elasticSeed)
        {
            _elasticSeed = elasticSeed;
        }

        private IElasticSeed _elasticSeed;
        [HttpPost]
        public IActionResult Seed()
        {
            try
            {
                _elasticSeed.Seed();
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
