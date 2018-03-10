using System.Collections.Generic;
using Mercedes_Benz.io_Challenge.ViewModels;
using MercedesManager;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using Domain.Classes;

namespace Mercedes_Benz.io_Challenge.Controllers
{
    [Route("api/[controller]")]
    public class DealersController : Controller
    {
        
        [ProducesResponseType(typeof(List<Dealer>), 200)]
        [HttpGet]
        public JsonResult Get() {
            return new JsonResult(Manager.GetDealers()) { StatusCode = (int) HttpStatusCode.OK };
        }
        [Route("closest")]
        [HttpPost]
        [ProducesResponseType(typeof(Dealer), 200)]
        [ProducesResponseType(typeof(string), 409)]
        public JsonResult Closest([FromBody] SearchViewModel model)
        {
            string[][] parameters = model.GetParameters();
            if (model.Latitude == null || model.Longitude == null)
                return new JsonResult("Latitude and Longitude are obligatory requirements") {StatusCode = (int) HttpStatusCode.BadRequest};
            return new JsonResult(Manager.FindDealersOrderedByDistance(parameters, ((double)model.Latitude, (double)model.Longitude)).FirstOrDefault()) { StatusCode = (int)HttpStatusCode.OK }; 
        }

        [Route("sorted")]
        [HttpPost]
        [ProducesResponseType(typeof(List<Dealer>), 200)]
        [ProducesResponseType(typeof(string), 409)]
        public JsonResult Sorted([FromBody] SearchViewModel model)
        {
            string[][] parameters = model.GetParameters();
            if (model.Latitude == null || model.Longitude == null)
                return new JsonResult("Latitude and Longitude are obligatory requirements") { StatusCode = (int)HttpStatusCode.BadRequest };
            return new JsonResult(Manager.FindDealersOrderedByDistance(parameters, ((double)model.Latitude, (double)model.Longitude))) { StatusCode = (int)HttpStatusCode.OK }; 
        }

        [Route("polygon")]
        [HttpPost]
        [ProducesResponseType(typeof(List<Dealer>), 200)]
        [ProducesResponseType(typeof(string), 409)]
        public JsonResult Polygon([FromBody] PolygonViewModel model)
        {
            string[][] parameters = model.GetParameters();
            if (model.Latitude == null || model.Longitude == null)
                return new JsonResult("Latitude and Longitude are obligatory requirements") { StatusCode = (int)HttpStatusCode.BadRequest };
            return new JsonResult(Manager.FindDealersInsidePolygon(parameters, model.GetCoordinates())) { StatusCode = (int)HttpStatusCode.OK }; 
        }
    }
}