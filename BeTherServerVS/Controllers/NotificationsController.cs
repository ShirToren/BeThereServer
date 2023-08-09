using BeTherServer.Models;
using BeTherServer.Services;
using BeTherServer.Services.NotificationsService;
using Microsoft.AspNetCore.Mvc;
namespace BeTherServer.Controllers
{
    [Controller]
    [Route("api/[controller]")]
    public class NotificationsController : Controller
    {
        private readonly INotificationsService m_NotificationsLogic;
        public NotificationsController(INotificationsService i_NotificationsLogic)
        {
            m_NotificationsLogic = i_NotificationsLogic;
        }

        [HttpGet]
        public IActionResult GetUsersNotifications(string UserName)
        {
            List<QuestionAsked> notificationsList = m_NotificationsLogic.GetUsersNotifications(UserName);

            try
            {
                return Ok(notificationsList);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }
        }
    }
}
