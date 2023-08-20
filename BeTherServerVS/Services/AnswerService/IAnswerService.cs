using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.AnswerService
{
    public interface IAnswerService
    {
        Task<ResultUnit<string>> PostNewAnswer(string i_UserName, UserAnswer i_AnswerToInsert);
        List<UserAnswer> GetUsersNewAnswers(string i_UserName);
    }
}
