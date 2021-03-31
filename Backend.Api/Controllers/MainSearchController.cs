using Backend.Module.Services;
using Backend.Module.Services.ElasticSearch.SearchImplementation;
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
    public class MainSearchController : ControllerBase
    {
        public MainSearchController(IElasticService elasticService)
        {
            _elasticSearch = new PropertyAndManagementSearch(elasticService);
        }

        private IElasticSearch _elasticSearch;
        [HttpGet]
        public ActionResult Get(string term, string? market, int page = 1)
        {
            try
            {
                Dictionary<string, object> arguments = new Dictionary<string, object>();
                arguments.Add("market", market);


                return Ok(_elasticSearch.Search(term, 25, page - 1, arguments));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
