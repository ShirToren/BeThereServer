using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.Utils;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BeTherServer.Controllers;

[Controller]
public class ConnectToAppController : Controller
{
    private readonly IConnectToAppService m_ConnectToAppLogic;

    public ConnectToAppController(IConnectToAppService i_ConnectToAppLogic)
    {
        m_ConnectToAppLogic = i_ConnectToAppLogic;
    }

    [Route("Login")]
    [HttpGet]
    public async Task<IActionResult> LoginUser(string UserName, string Password)
    {
        try
        {
            ResultUnit<UserData> result = await m_ConnectToAppLogic.LoginToApp(UserName, Password);
            if (result.IsSuccess)
            {
                return Ok(result.ReturnValue);
            }
            else
            {
                return NotFound(result.ResultMessage);
            }
        }
        catch (Exception ex)
        {
            string exptionError = ex.Message;
            return StatusCode(500);

        }
    }

    [Route("CreateAccount")]
    [HttpPost]
    public async Task<IActionResult> CreateNewAccount([FromBody] UserData NewUser)
    {
        try
        {
            ResultUnit<string> result = await m_ConnectToAppLogic.CreateNewUserAccount(NewUser);
            if (result.IsSuccess == false)
            {
                return NotFound(result.ResultMessage);
            }
            else
            {
                return Ok();
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }

}

