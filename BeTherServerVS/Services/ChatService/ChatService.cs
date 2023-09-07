using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.ChatService
{
    public class ChatService : IChatService
    {
        private IChatMessagesDBContext m_ChatMessagesDatabaseService;
        private readonly Dictionary<string, HashSet<string>> userRooms = new Dictionary<string, HashSet<string>>();


        public ChatService(IChatMessagesDBContext i_ChatMessagesDatabaseService)
        {
            m_ChatMessagesDatabaseService = i_ChatMessagesDatabaseService;
            if (userRooms.Count == 0)
            {
                //fetch from data base
            }
        }
        public async Task<ResultUnit<List<ChatMessage>>> GetMessagesByChatRoomId(string i_ChatRoom)
        {
            ResultUnit<List<ChatMessage>> result = new ResultUnit<List<ChatMessage>>();
            List<ChatMessage> list = await m_ChatMessagesDatabaseService.GetMessagesByChatRoom(i_ChatRoom);

            if (list != null)
            {
                result.IsSuccess = true;
                result.ReturnValue = list;
            }
            else
            {
                result.IsSuccess = false;
            }

            return result;
        }

        Dictionary<string, HashSet<string>> IChatService.GetUserRooms()
        {
            return userRooms;
        }
    }
}
