using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.ChatService
{
    public interface IChatService
    {
        Task<ResultUnit<List<ChatMessage>>> GetMessagesByChatRoomId(string i_ChatRoom);
    }
}
