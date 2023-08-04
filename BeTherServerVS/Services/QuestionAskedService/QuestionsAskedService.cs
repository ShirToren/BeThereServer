using BeTherServer.Models;
using Microsoft.Extensions.Options;
using BeTherServer.Services.Utils;
using BeTherServer.Services;
using BeTherServer.Services.UpdateLocationService;

namespace BeTherMongoDB.Services;

public class QuestionsAskedService :IQuestionsAskedService
{

    IQuestionAskedDBContext m_QuestionsAskedDBContext;

    public QuestionsAskedService(IQuestionAskedDBContext i_QuestionsAskedDBSContext)
    {
        m_QuestionsAskedDBContext = i_QuestionsAskedDBSContext;
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
        List<string> relevantUsers = new List<string>();
        IUpdateLocationService updateLocationService = new UpdateLocationService();
        Dictionary<string, Location> locations = updateLocationService.GetLocations();
        foreach (var kvp in locations)
        {
            string userName = kvp.Key;
            Location location = kvp.Value;
            if (i_QuestionAskedToInsert.locationLatitude.Equals(location.latitude) && 
                i_QuestionAskedToInsert.locationLongitude.Equals(location.longitude))
            {
                relevantUsers.Add(userName);
            }
        }
        result.ReturnValue = relevantUsers;
        result.IsSuccess = true;
        return result;
    }
}
