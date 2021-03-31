using Backend.Module.BusinessModels;
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
    public class PropertyController : ControllerBase
    {
        public PropertyController(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        private IElasticService _elasticService;

        [HttpPost]
        [HttpPut]
        public IActionResult IndexingPropertyDocument([FromBody] List<PropertyContainer> model)
        {
            try
            {
                ServiceResponse response = _elasticService.IndexingDocuments<PropertyContainer>("properties", model);
                if (response.Success)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(response.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
    }
}
