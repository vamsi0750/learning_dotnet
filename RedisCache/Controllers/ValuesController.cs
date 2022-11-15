using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;

namespace RedisCache.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IDistributedCache _distributedCache;
        public ValuesController(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        private byte[] GetNumbers()
        {
            return new byte[] { 1, 3, 4, 4, 4, 4, 3 };
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var nums = await _distributedCache.GetAsync("data");
            if(nums is null)
            {
                nums =  GetNumbers();
                var options = new DistributedCacheEntryOptions();
                options.SetAbsoluteExpiration(TimeSpan.FromMinutes(2));
                await _distributedCache.SetAsync("data",nums,options);
            }
            
            return Ok(nums);
        }
    }
}
