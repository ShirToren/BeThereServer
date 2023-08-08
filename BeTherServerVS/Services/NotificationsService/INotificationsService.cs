using BeTherServer.Models;

namespace BeTherServer.Services.NotificationsService
{
    public interface INotificationsService
    {
        List<QuestionAsked> GetUsersNotifications(string i_UserName);
        void AddNotification(string i_UserName, QuestionAsked i_Question);
    }
}
