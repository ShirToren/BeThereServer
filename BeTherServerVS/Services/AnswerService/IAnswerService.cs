using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services.AnswerService
{
    public interface IAnswerService
    {
        Task<ResultUnit<string>> PostNewAnswer(string i_UserName, Answer i_AnswerToInsert);
        List<Answer> GetUsersNewAnswers(string i_UserName);
    }
}
