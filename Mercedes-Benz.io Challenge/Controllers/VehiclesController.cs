using System.Collections.Generic;
using System.Net;
using MercedesManager;
using Mercedes_Benz.io_Challenge.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mercedes_Benz.io_Challenge.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : Controller {
        [HttpGet]
        public JsonResult Get() {
            return new JsonResult(Manager.GetVehicles()) { StatusCode = (int)HttpStatusCode.OK };
        }
        [Route("listall")]
        [HttpPost]
        public JsonResult ListAll([FromBody]SearchViewModel model) {
            string[][] parameters = model.GetParameters();
            return new JsonResult(Manager.GetVehiclesByParameter(parameters)) { StatusCode = (int)HttpStatusCode.OK };
        } 
    }
}