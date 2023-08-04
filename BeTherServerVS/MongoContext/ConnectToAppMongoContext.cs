using BeTherServer.Models;
using Microsoft.Extensions.Options;
using BeTherServer.Services;

namespace BeTherServer.MongoContext
{
    public class ConnectToAppMongoContext : BaseMongoContext<UserData>, IConnectToAppDBContext
    {
        private static readonly string m_collectionName = "UsersData";


        public ConnectToAppMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings) : base(i_mongoDBSettings, m_collectionName)
        {

        }
        public async Task<UserData> GetUserByUsername(string i_username)
        {
            var responseUser = await base.Collection.Find(x => x.username == i_username).FirstOrDefaultAsync();
            return responseUser;
        }

        public async Task<List<UserData>> GetAllUsers()
        {
            List<UserData> usersToReturn = new List<UserData>();
            List<UserData> responseUsers = await base.Collection.Find(new BsonDocument()).ToListAsync();
            foreach(UserData user in responseUsers)
            {
                usersToReturn.Add(user);
            }
            return usersToReturn;

        }

        public async Task CreateNewUser(UserData i_NewUserData)
        {
            await base.Collection.InsertOneAsync(i_NewUserData);
        }
    }
}

