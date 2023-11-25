using BeTherServer.Models;
using BeTherServer.Services.UserRoomsService;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeTherServer.MongoContext
{
    public class UserRoomsMongoContext : BaseMongoContext<UserRooms>, IUserRoomsDBContext
    {
        private static readonly string m_collectionName = "UserRooms";

        public UserRoomsMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings) : base(i_mongoDBSettings, m_collectionName)
        {

        }

        public async Task InsertUserRoom(string i_UserName, string i_ChatRoomId)
        {

            FilterDefinition<UserRooms> filter = Builders<UserRooms>.Filter.Eq("username", i_UserName);

            UserRooms response = await base.FindObjectByFilter(filter);
            if (response == null)
            {
                UserRooms userRooms = new UserRooms();
                userRooms.username = i_UserName;
                userRooms.rooms = new HashSet<string>();
                userRooms.rooms.Add(i_ChatRoomId);
                await base.InsertOneObject(userRooms);
            }
            else
            {
                var existingHashSet = response.rooms;
                existingHashSet.Add(i_ChatRoomId);
                UpdateDefinition<UserRooms> update = Builders<UserRooms>.Update.Set("rooms", existingHashSet);
                base.Collection.UpdateOne(filter, update);
            }
            return;
        }

        public async Task<HashSet<string>> GetRoomsByUserName(string i_Username)
        {
            var responseUser = await base.Collection.Find(x => x.username == i_Username).FirstOrDefaultAsync();
            return responseUser.rooms;
        }
    }
}
