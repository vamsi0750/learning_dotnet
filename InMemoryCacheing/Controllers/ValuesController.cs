using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace InMemoryCacheing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IMemoryCache _memoryCacheing;
        public ValuesController(IMemoryCache memoryCacheing)
        {
            _memoryCacheing = memoryCacheing;
        }

        private int[] GetNumbers()
        {
            return new int[] { 1, 2, 3, 3, 4, 4 };
        }

        [HttpGet]
        public IActionResult Get()
        {
            int[] result;
            if(!_memoryCacheing.TryGetValue("data", out result)) 
            {
                result = GetNumbers();
                _memoryCacheing.Set("data", result,System.TimeSpan.FromMinutes(2));
            }
            return Ok(result);
        }
    }
}
