using BeTherServer.Models;
using BeTherServer.Services;
using Microsoft.Extensions.Options;

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
    }
}
