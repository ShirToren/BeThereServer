using BeTherServer.Models;

namespace BeTherServer.Services.ChatService
{
    public interface IChatMessagesDBContext
    {
        Task SaveMessage(ChatMessage chatMessage);
        Task<List<ChatMessage>> GetMessagesByChatRoom(string chatRoomId);
    }
}
