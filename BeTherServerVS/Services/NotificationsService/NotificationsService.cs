using BeTherServer.Models;

namespace BeTherServer.Services.NotificationsService
{
    public class NotificationsService : INotificationsService
    {
        private readonly Dictionary<string, List<QuestionAsked>> m_Notifications = new Dictionary<string, List<QuestionAsked>>();
        private static readonly object sr_DictionaryLock = new object();


        public List<QuestionAsked> GetUsersNotifications(string i_UserName)
        {
            lock (sr_DictionaryLock)
            {
                List<QuestionAsked> notifications = new List<QuestionAsked>();
                if (!m_Notifications.ContainsKey(i_UserName))
                {
                    return notifications;
                }
                else
                {
                    notifications = m_Notifications[i_UserName];
                    m_Notifications.Remove(i_UserName);
                    return notifications;
                }
            }
        }

        public void AddNotification(string i_UserName, QuestionAsked i_Question)
        {
            lock(sr_DictionaryLock)
            {
                if (m_Notifications.ContainsKey(i_UserName))
                {
                    m_Notifications[i_UserName].Add(i_Question);
                }
                else
                {
                    m_Notifications.Add(i_UserName, new List<QuestionAsked>());
                    m_Notifications[i_UserName].Add(i_Question);
                }
            }
        }
    }
}
