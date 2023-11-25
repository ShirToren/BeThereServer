using BeTherServer.Models;
using BeTherServer.Services.ChatService;
using Microsoft.Extensions.Options;

namespace BeTherServer.MongoContext
{
    public class ChatMessagesMongoContext : BaseMongoContext<ChatMessage>, IChatMessagesDBContext
    {
        private static readonly string m_collectionName = "ChatMessages";
        public ChatMessagesMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings) : base(i_mongoDBSettings, m_collectionName)
        {

        }

        public async Task SaveMessage(ChatMessage chatMessage)
        {
            await base.Collection.InsertOneAsync(chatMessage);
        }

        public async Task<List<ChatMessage>> GetMessagesByChatRoom(string chatRoomId)
        {
            List<ChatMessage> messagesToReturn = new List<ChatMessage>();
            var response = await base.Collection.Find(x => x.ChatRoomId == chatRoomId).ToListAsync();
            foreach (var message in response)
            {
                messagesToReturn.Add(message);
            }
            return messagesToReturn;
        }
        public async Task<ChatMessage> GetLastMessageByChatRoom(string chatRoomId)
        {
            ChatMessage response = await base.Collection.Find(x => x.ChatRoomId == chatRoomId).FirstOrDefaultAsync();
            return response;
        }
    }
}
