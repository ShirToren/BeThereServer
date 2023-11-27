using BeTherServer.Models;

namespace BeTherServer.Services
{
    public interface IUserDBContext
    {
        Task<UserData> GetUserByUsername(string i_username);
        Task UpdateUsersCreditsAsync(string i_UserName, int i_Credits);
    }
}
