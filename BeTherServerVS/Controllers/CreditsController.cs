using BeTherServer.Services.CreditsService;
using BeTherServer.Services.UserRoomsService;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Conventions;

namespace BeTherServer.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class CreditsController : Controller
    {
        private readonly ICreditsService r_CreditsService;

        public CreditsController(ICreditsService i_CreditsService)
        {
            r_CreditsService = i_CreditsService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string UserName, int Credits)
        {
            try
            {
                await r_CreditsService.UpdateUsersCredits(UserName, Credits);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
