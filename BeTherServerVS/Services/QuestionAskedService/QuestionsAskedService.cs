using BeTherServer.Models;
using Microsoft.Extensions.Options;
using BeTherServer.Services.Utils;
using BeTherServer.Services;
using BeTherServer.Services.UpdateLocationService;
using BeTherServer.Services.NotificationsService;
using System;


namespace BeTherMongoDB.Services;

public class UserQuestionsService :IAskedQuestionService
{
    private const double EarthRadiusKm = 6371.0; // Earth's radius in kilometers

    private IAskedQuestionDBContext m_QuestionsAskedDBContext;
    private IQuestionAnswersDBContext m_QuestionAnswersDBContext;
    private IUserDBContext m_UserDBContext;
    private IUpdateLocationService m_UpdateLocationService;
    private INotificationsService m_NotificationsService;



    public UserQuestionsService(IAskedQuestionDBContext i_QuestionsAskedDBSContext, IUpdateLocationService i_UpdateLocationService, INotificationsService i_NotificationsService, IQuestionAnswersDBContext i_QuestionsAnswersDBContext, IUserDBContext i_UserDBContext)
    {
        m_QuestionsAskedDBContext = i_QuestionsAskedDBSContext;
        m_UpdateLocationService = i_UpdateLocationService;
        m_NotificationsService = i_NotificationsService;
        m_QuestionAnswersDBContext = i_QuestionsAnswersDBContext;
        m_UserDBContext = i_UserDBContext;
    }

    public async Task<ResultUnit<Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>>> GetUsersQuestionsAndAnswers(string i_username)
    {
        ResultUnit<Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>> result = new ResultUnit<Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>>();
        Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>> QuestionsAndAnswers = new Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>();

        List<QuestionAsked> usersPreviousQuestions = await m_QuestionsAskedDBContext.GetPerviousQuestionsByUser(i_username);
        foreach(QuestionAsked question in usersPreviousQuestions)
        {
            QuestionAnswers AllQuestionsAnswers = await m_QuestionAnswersDBContext.GetAnswersByQuestionId(question.questionId);
            Tuple<QuestionAsked, QuestionAnswers> questionAndAnswersTuple = new Tuple<QuestionAsked, QuestionAnswers>(question, AllQuestionsAnswers);
            QuestionsAndAnswers.Add(question.questionId, questionAndAnswersTuple);
        }
        
        if(usersPreviousQuestions is null)
        {
            result.IsSuccess = false;
            result.ResultMessage = "No previous questions";
        }
        else
        {
            result.ReturnValue = QuestionsAndAnswers;
        }

        return result;
    }

    public async Task<ResultUnit<List<UserAnswer>>> GetListOfAnswers(string i_QuestionId)
    {
        ResultUnit<List<UserAnswer>> result = new ResultUnit<List<UserAnswer>>();
        List<UserAnswer> list = new List<UserAnswer>();

            QuestionAnswers AllQuestionsAnswers = await m_QuestionAnswersDBContext.GetAnswersByQuestionId(i_QuestionId);
            list = AllQuestionsAnswers.userAnswer;

            result.ReturnValue = list;
        result.IsSuccess = true;
        return result;
    }

    public async Task<ResultUnit<string>> InsertQuestionAsked(QuestionAsked i_QuestionAskedToInsert)
    {
        ResultUnit<string> result = new ResultUnit<string>();

        //long uniqueQuestionId = generateTimestampBasedId();

        await m_QuestionsAskedDBContext.InsertNewQuestionAsked(i_QuestionAskedToInsert);
        //create new answers object for this question, with empty list
        await m_QuestionAnswersDBContext.CreateNewQuestionAnswersItem(i_QuestionAskedToInsert.questionId);

        Dictionary<string, LocationData> locations = m_UpdateLocationService.GetLocations();
        foreach (var kvp in locations)
        {
            string userName = kvp.Key;
            LocationData location = kvp.Value;
            double distance = calculateDistance(i_QuestionAskedToInsert.location.latitude, i_QuestionAskedToInsert.location.longitude,
                location.latitude, location.longitude);
            distance = distance * 1000;
            double radiusKm = i_QuestionAskedToInsert.radius; // Radius in kilometers
            if (distance <= radiusKm && await isUserFitsFilters(i_QuestionAskedToInsert, userName))
            {
                m_NotificationsService.AddNotification(userName, i_QuestionAskedToInsert);
            }
        }

        result.IsSuccess = true;
        return result;
    }

    private async Task<bool> isUserFitsFilters(QuestionAsked i_Question, string i_UserName)
    {
        bool isUserFits = true;
        UserData user = await m_UserDBContext.GetUserByUsername(i_UserName);
        if(i_Question.gender != null && !i_Question.gender.Equals(user.gender))
        {
            isUserFits = false;
        }
        if (i_Question.minimumAgeRange != null && user.age < i_Question.minimumAgeRange)
        {
            isUserFits = false;
        }
        if (i_Question.maximumAgeRange != null && user.age > i_Question.maximumAgeRange)
        {
            isUserFits = false;
        }
        return isUserFits;
    }

    private double calculateDistance(double lat1, double lon1, double lat2, double lon2)
    {
        var dLat = DegreeToRadians(lat2 - lat1);
        var dLon = DegreeToRadians(lon2 - lon1);

        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(DegreeToRadians(lat1)) * Math.Cos(DegreeToRadians(lat2)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        return EarthRadiusKm * c;
    }

    private double DegreeToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }


    //delete
    public async Task InsertQuestionAnswer(QuestionAnswers i_QuestionAskedToInsert)
    {
        await m_QuestionAnswersDBContext.InsertNewQuestionAnswers(i_QuestionAskedToInsert);

    }

    private long generateTimestampBasedId()
    {
        TimeSpan timeSpan; // Declare the variable outside the lock block
        DateTime epoch = new DateTime(1970, 1, 1);
        timeSpan = DateTime.UtcNow - epoch; // Assign the value within the lock block

        return (long)timeSpan.TotalMilliseconds;
    }
}
