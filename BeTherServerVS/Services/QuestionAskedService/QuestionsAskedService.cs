using BeTherServer.Models;
using Microsoft.Extensions.Options;
using BeTherServer.Services.Utils;
using BeTherServer.Services;
using BeTherServer.Services.UpdateLocationService;
using BeTherServer.Services.NotificationsService;

namespace BeTherMongoDB.Services;

public class QuestionsAskedService :IQuestionsAskedService
{

    IQuestionAskedDBContext m_QuestionsAskedDBContext;
    IUpdateLocationService m_UpdateLocationService;
    INotificationsService m_NotificationsService;

    public QuestionsAskedService(IQuestionAskedDBContext i_QuestionsAskedDBSContext, IUpdateLocationService i_UpdateLocationService, INotificationsService i_NotificationsService)
    {
        m_QuestionsAskedDBContext = i_QuestionsAskedDBSContext;
        m_UpdateLocationService = i_UpdateLocationService;
        m_NotificationsService = i_NotificationsService;
    }

    public async Task<ResultUnit<List<QuestionAsked>>> GetUsersPreviousQuestions(string i_username)
    {
        ResultUnit<List<QuestionAsked>> result = new ResultUnit<List<QuestionAsked>>();
        List<QuestionAsked> usersPreviousQuestions = await m_QuestionsAskedDBContext.GetPerviousQuestionsByUser(i_username);
        if(usersPreviousQuestions is null)
        {
            result.IsSuccess = false;
            result.ResultMessage = "No previous questions";
        }
        else
        {
            result.ReturnValue = usersPreviousQuestions;
        }

        return result;
    }

    public async Task<ResultUnit<List<string>>> InsertQuestionAsked(QuestionAsked i_QuestionAskedToInsert)
    {
        ResultUnit<List<string>> result = new ResultUnit<List<string>>();
        await m_QuestionsAskedDBContext.InsertNewQuestionAsked(i_QuestionAskedToInsert);
        Dictionary<string, Location> locations = m_UpdateLocationService.GetLocations();
        foreach (var kvp in locations)
        {
            string userName = kvp.Key;
            Location location = kvp.Value;
            if (i_QuestionAskedToInsert.location.latitude.Equals(location.latitude) && 
                i_QuestionAskedToInsert.location.longitude.Equals(location.longitude))
            {
                m_NotificationsService.AddNotification(userName, i_QuestionAskedToInsert);
            }
        }
        //result.ReturnValue = relevantUsers;
        result.IsSuccess = true;
        return result;
    }
}
