using BeTherServer.Models;
using BeTherServer.Services.ChatService;
using BeTherServer.Services.UserRoomsService;
using BeTherServer.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BeTherServer.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class UserRoomsController : Controller
    {
        private readonly IUserRoomsService r_UserRoomsService;
        public UserRoomsController(IUserRoomsService i_UserRoomsService)
        {
            r_UserRoomsService = i_UserRoomsService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(string UserName, string ChatRoomId)
        {
            try
            {
                await r_UserRoomsService.InsertUserRoom(UserName, ChatRoomId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetChatRoomsByUserName(string UserName)
        {
            ResultUnit<HashSet<string>> result = await r_UserRoomsService.GetChatRoomsByUserName(UserName);
            try
            {
                if (result.IsSuccess)
                {
                    return Ok(result.ReturnValue);

                }
                else
                {
                    return NotFound(result.ResultMessage);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
