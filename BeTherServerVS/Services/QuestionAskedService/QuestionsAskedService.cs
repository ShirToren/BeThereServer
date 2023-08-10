using BeTherServer.Models;
using Microsoft.Extensions.Options;
using BeTherServer.Services.Utils;
using BeTherServer.Services;
using BeTherServer.Services.UpdateLocationService;
using BeTherServer.Services.NotificationsService;


namespace BeTherMongoDB.Services;

public class QuestionsAskedService :IQuestionsAskedService
{
    private const double EarthRadiusKm = 6371.0; // Earth's radius in kilometers

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
        Dictionary<string, LocationData> locations = m_UpdateLocationService.GetLocations();
        foreach (var kvp in locations)
        {
            string userName = kvp.Key;
            LocationData location = kvp.Value;
/*            double distance = CalculateDistance(i_QuestionAskedToInsert.location.latitude, i_QuestionAskedToInsert.location.longitude,
                location.latitude, location.longitude);
            double radiusKm = 0.5; // Radius in kilometers
            if (distance <= radiusKm)
            {
                ///inside
            }*/
            if (i_QuestionAskedToInsert.location.latitude.Equals(location.latitude) && 
                i_QuestionAskedToInsert.location.longitude.Equals(location.longitude))
            {
                m_NotificationsService.AddNotification(userName, i_QuestionAskedToInsert);
            }
        }
        result.IsSuccess = true;
        return result;
    }

    public double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
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
}
