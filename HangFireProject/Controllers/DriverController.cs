using Hangfire;
using HangFireProject.Models;
using HangFireProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HangFireProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private static List<Driver> drivers = new List<Driver>();
        public DriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IActionResult AddDriver(Driver driver)
        {
            if (ModelState.IsValid)
            {
                drivers.Add(driver);
                var jobId = BackgroundJob.Enqueue<IServicemanagement>(x => x.SendMail());
                return CreatedAtAction("GetDriver",new {driver.Id},driver);
            }
            return BadRequest();
        }

        [HttpGet("{id}")]
        public IActionResult GetDriver(Guid id)
        {
            var driver = drivers.FirstOrDefault(x => x.Id == id);
            if(driver is null)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpGet]
        public IActionResult GetAllDrivers()
        {
            var driver = drivers.ToList();
            if (driver.Count is 0)
            {
                return NotFound();
            }
            return Ok(driver);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDriver(Guid id)
        {
            var driver = drivers.FirstOrDefault(x => x.Id == id);
            if(driver is null)
            {
                return NotFound();
            }
            driver.Status = 0;
            RecurringJob.AddOrUpdate<IServicemanagement>(s => s.UpdateDatebase(), Cron.Hourly);
            return NotFound();

        }
    }
}
