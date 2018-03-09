using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Classes;
using MercedesManager;
using Mercedes_Benz.io_Challenge.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Mercedes_Benz.io_Challenge.Controllers
{
    [Route("api/[controller]")]
    public class BookingsController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult Get() {
            return new JsonResult(Manager.GetBookings()) { StatusCode = (int)HttpStatusCode.OK };
        }
        [Route("create")]
        [HttpPost]
        public ActionResult Create([FromBody]Booking input) {

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
                    return StatusCode((int)HttpStatusCode.OK);
                default:
                    return StatusCode((int)HttpStatusCode.InternalServerError);
            }

        }

        [Route("cancel")]
        [HttpPost]
        public ActionResult Cancel([FromBody] CancelBookingViewModel input) {
            return Manager.CancelBooking(input.Id, input.CancelledAt, input.CancelledReason) ? 
                StatusCode((int)HttpStatusCode.OK) : 
                StatusCode((int)HttpStatusCode.BadRequest);
        }
    }
}