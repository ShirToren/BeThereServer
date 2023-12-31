﻿using System;
using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services
{
	public class ConnectToAppService : IConnectToAppService
	{
        
        private readonly IConnectToAppDBContext r_UserInfoDatabaseService;

		public ConnectToAppService(IConnectToAppDBContext i_UserInfoDatabaseService)
		{
            r_UserInfoDatabaseService = i_UserInfoDatabaseService;
        }

		public async Task<ResultUnit<UserData>> LoginToApp(string i_username, string i_password)
		{
            ResultUnit<UserData> resultHelper = new ResultUnit<UserData>();
            UserData userByUsername = await r_UserInfoDatabaseService.GetUserByUsername(i_username);
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
            List<UserData> usersData = await r_UserInfoDatabaseService.GetAllUsers();

            if (checkIfUsernameFreeToUse(i_NewUser.username, usersData) == false)
            {
                resultHelper.IsSuccess = false;
                resultHelper.ResultMessage = "Username is in use";
            }
            else
            {
                await r_UserInfoDatabaseService.CreateNewUser(i_NewUser);
            }

            return resultHelper;
        }

        public async Task<ResultUnit<UserData>> GetUserData(string i_UserName)
        {
            ResultUnit<UserData> resultHelper = new ResultUnit<UserData>();
            UserData usersData = await r_UserInfoDatabaseService.GetUserByUsername(i_UserName);

            if (usersData != null)
            {
                resultHelper.IsSuccess = true;
                resultHelper.ReturnValue = usersData;
            }
            else
            {
                resultHelper.IsSuccess = false;
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

