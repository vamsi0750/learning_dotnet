using AutoMapperProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoMapperProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly ILogger<DriverController> _logger;
        private readonly static List<Driver> drivers =  new List<Driver>();
        public DriverController(ILogger<DriverController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllDrivers()
        {
            var allDrivers = drivers.Where(x=> x.Status == 1).ToList();
            if(allDrivers.Count is 0)
            {
                return NoContent();
            }
            return Ok(allDrivers);
        }

        [HttpPost]
        public IActionResult CreateDriver([FromBody] Driver newDriver)
        {
            drivers.Add(newDriver);
            return Created("", newDriver);
            //return new JsonResult("Something went wrong") { StatusCode = 500 };
        }

        [HttpGet("{id}")]
        public IActionResult GetDriverById(Guid id)
        {
            var existingDriver = drivers.FirstOrDefault(d => d.Id == id);
            if (existingDriver is null)
            {
                return NotFound();
            }
            return Ok(existingDriver);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateDriver(Guid id, [FromBody] Driver driverData)
        {
            var existingDriver = drivers.FirstOrDefault(d => d.Id == id);
            if (existingDriver is null)
            {
                return NotFound();
            }
            existingDriver.DriverNumber = driverData.DriverNumber;
            existingDriver.DateUpdated = DateTime.Now;
            existingDriver.FirstName = driverData.FirstName;
            existingDriver.LastName = driverData.LastName;
            existingDriver.WorldChampionships = driverData.WorldChampionships;
            return NoContent();
            
        }

        [HttpDelete("{id}")]
        public IActionResult UpdateDriver(Guid id)
        {
            var existingDriver = drivers.FirstOrDefault(d => d.Id == id);
            if (existingDriver is null)
            {
                return NotFound();
            }
            existingDriver.Status = 0;
            return NoContent();
        }


    }
}
