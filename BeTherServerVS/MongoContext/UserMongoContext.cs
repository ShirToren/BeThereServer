using BeTherServer.Models;
using BeTherServer.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeTherServer.MongoContext
{
    public class UserMongoContext : BaseMongoContext<UserData>, IUserDBContext
    {
        private static readonly string m_collectionName = "UsersData";

        public UserMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings) : base(i_mongoDBSettings, m_collectionName)
        {

        }

        public async Task<UserData> GetUserByUsername(string i_username)
        {
            var responseUser = await base.Collection.Find(x => x.username == i_username).FirstOrDefaultAsync();
            return responseUser;
        }
        public async Task UpdateUsersCreditsAsync(string i_UserName, int i_Credits)
        {
            FilterDefinition<UserData> idFilter = Builders<UserData>.Filter.Eq("username", i_UserName);
            UpdateDefinition<UserData> idUpdate = Builders<UserData>.Update.Inc("credits", i_Credits);
            await base.Collection.UpdateOneAsync(idFilter, idUpdate);
            return;
        }
    }
}
