using System.Collections.Generic;
using System.Net;
using Domain.Classes;
using MercedesManager;
using Mercedes_Benz.io_Challenge.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Mercedes_Benz.io_Challenge.Controllers
{
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        [HttpGet]
        [ProducesResponseType(typeof(List<Booking>), 200)]
        public JsonResult Get()
        {
            return new JsonResult(Manager.GetBookings()) { StatusCode = (int)HttpStatusCode.OK };
        }
        [Route("create")]
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public ActionResult Create([FromBody]Booking input)
        {

            switch (Manager.AddBooking(input)) {
                case "Duplicate":
                    return StatusCode((int) HttpStatusCode.Conflict);
                case "Vehicle does not exist":
                    return StatusCode((int) HttpStatusCode.BadRequest);
                case "Schedule time is not available":
                    return StatusCode((int)HttpStatusCode.BadRequest);
                case "Unknown Error":
                    return StatusCode((int) HttpStatusCode.InternalServerError);
                case "OK":
                    return StatusCode((int)HttpStatusCode.Created);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [Route("cancel")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(409)]
        public ActionResult Cancel([FromBody] CancelBookingViewModel input) {
            return Manager.CancelBooking(input.Id, input.CancelledAt, input.CancelledReason) ? 
                StatusCode((int)HttpStatusCode.OK) : 
                StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}