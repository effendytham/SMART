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
    public class ManagementController : ControllerBase
    {
        public ManagementController(IElasticService elasticService)
        {
            _elasticService = elasticService;
        }

        private IElasticService _elasticService;

        [HttpPost]
        [HttpPut]
        public IActionResult IndexingManagementDocument([FromBody] List<ManagementContainer> model)
        {
            try
            {
                ServiceResponse response = _elasticService.IndexingDocuments<ManagementContainer>("managements", model);
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
