using Microsoft.AspNetCore.Mvc;
using Nest;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ElasticSearch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IElasticClient _elasticClient; 
        public UserController(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }
      
        [HttpGet("{id}")]
        public async Task<User> GetAsync(string id)
        {
            var response = await _elasticClient.GetAsync<User>(id,x=>x.Index("users"));

            return response?.Source;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] User value)
        {
            var ressult = await _elasticClient.IndexAsync<User>(value, x => x.Index("users"));
            return Created("",ressult.Id);
        }
    }
}
