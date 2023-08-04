using System;
using BeTherServer.Models;

namespace BeTherServer.Services
{
	public interface IConnectToAppDBContext
	{
        Task<UserData> GetUserByUsername(string i_username);
        Task<List<UserData>> GetAllUsers();
        Task CreateNewUser(UserData i_NewUserData);

    }
}

