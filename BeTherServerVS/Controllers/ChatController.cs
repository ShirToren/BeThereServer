using BeTherServer.Models;
using BeTherServer.Services.ChatService;
using BeTherServer.Services.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BeTherServer.Controllers
{
    [Controller]
    [Route("api/ChatMessages")]
    public class ChatController : Controller
    {
        private readonly IChatService r_ChatService;
        public ChatController(IChatService i_ChatService)
        {
            r_ChatService =  i_ChatService;
        }
        [HttpGet]
        public async Task<IActionResult> GetMessagesByChatRoomId(string ChatRoomId)
        {
            ResultUnit<List<ChatMessage>> result = await r_ChatService.GetMessagesByChatRoomId(ChatRoomId);
            try
            {
                return Ok(result.ReturnValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
        [Route("Last")]
        [HttpGet]
        public async Task<IActionResult> GetLastMessageByChatRoomId(string ChatRoomId)
        {
            ResultUnit<ChatMessage> result = await r_ChatService.GetLastMessageByChatRoomId(ChatRoomId);
            try
            {
                return Ok(result.ReturnValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
