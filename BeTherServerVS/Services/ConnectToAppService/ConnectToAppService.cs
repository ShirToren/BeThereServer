using System;
using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services
{
	public class ConnectToAppService : IConnectToAppService
	{
        
        IConnectToAppDBContext m_UserInfoDatabaseService;

		public ConnectToAppService(IConnectToAppDBContext i_UserInfoDatabaseService)
		{
            m_UserInfoDatabaseService = i_UserInfoDatabaseService;
        }

		public async Task<ResultUnit<UserData>> LoginToApp(string i_username, string i_password)
		{
            ResultUnit<UserData> resultHelper = new ResultUnit<UserData>();
            UserData userByUsername = await m_UserInfoDatabaseService.GetUserByUsername(i_username);
            if (userByUsername is null)
            {
                resultHelper.IsSuccess = false;
                resultHelper.ResultMessage = "No user found";
            }
            else if (userByUsername.password != i_password)
            {
                resultHelper.IsSuccess = false;
                resultHelper.ResultMessage = "Incorect password";
            }

            resultHelper.ReturnValue = userByUsername;
            return resultHelper;
        }

        public async Task<ResultUnit<string>> CreateNewUserAccount(UserData i_NewUser)
        {
            ResultUnit<string> resultHelper = new ResultUnit<string>();
            List<UserData> usersData = await m_UserInfoDatabaseService.GetAllUsers();

            if (checkIfUsernameFreeToUse(i_NewUser.username, usersData) == false)
            {
                resultHelper.IsSuccess = false;
                resultHelper.ResultMessage = "Username is in use";
            }
            else
            {
               
                await m_UserInfoDatabaseService.CreateNewUser(i_NewUser);
            }

            return resultHelper;
        }
            

        private bool checkIfUsernameFreeToUse(string i_Username, List<UserData> usersData)
        {
            bool isFreeToUse = true;

            foreach (UserData user in usersData)
            {
                if (i_Username == user.username)
                {
                    isFreeToUse = false;
                    break;
                }
            }

            return isFreeToUse;
        }
    }
}

