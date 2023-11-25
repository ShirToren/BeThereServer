using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.ChatService
{
    public interface IChatService
    {
        Task<ResultUnit<List<ChatMessage>>> GetMessagesByChatRoomId(string i_ChatRoom);
        Dictionary<string, HashSet<string>> GetUserRooms();
        Task<ResultUnit<ChatMessage>> GetLastMessageByChatRoomId(string i_ChatRoom);
    }
}
