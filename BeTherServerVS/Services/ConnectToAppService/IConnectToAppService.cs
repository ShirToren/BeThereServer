using System;
using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services
{
	public interface IConnectToAppService
    {
        Task<ResultUnit<UserData>> LoginToApp(string i_username, string i_password);
        Task<ResultUnit<string>> CreateNewUserAccount(UserData i_NewUser);
    }
}

