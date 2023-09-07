using BeTherServer.Models;

namespace BeTherServer.Services
{
    public interface IUserDBContext
    {
        Task<UserData> GetUserByUsername(string i_username);
    }
}
