using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.UpdateLocationService;
using BeTherServer.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BeTherServer.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UpdateLocationController : Controller
    {
        private readonly IUpdateLocationService m_UpdateLocationLogic;
        public UpdateLocationController(IUpdateLocationService i_UpdateLocationLogic)
        {
            m_UpdateLocationLogic = i_UpdateLocationLogic;
        }

        [HttpGet]
        public IActionResult GetAllLocations()
        {
            Dictionary<string, LocationData> locations = m_UpdateLocationLogic.GetLocations();
        
            try
            {
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }
        [HttpPost]
        public IActionResult Post(string UserName, [FromBody] LocationData i_Location)
        {
                m_UpdateLocationLogic.UpdateCurrentLocation(UserName, i_Location);
                return Ok();
        }
    }
}
