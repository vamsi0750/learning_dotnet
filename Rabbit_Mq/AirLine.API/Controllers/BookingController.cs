using AirLine.API.Models;
using AirLine.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AirLine.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IMessageProducer _messageProducer;

        //In-Memory Db
        public static List<Booking> bookings = new();

        public BookingController(ILogger<BookingController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CraeteBooking(Booking newBooking)
        {
            bookings.Add(newBooking);
            _messageProducer.SendingMessage<Booking>(newBooking);
            return Ok(newBooking.Id);
        }

    }
}
