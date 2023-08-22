using System;
using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services
{
	public interface IAskedQuestionService
	{
        Task<ResultUnit<Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>>> GetUsersQuestionsAndAnswers(string i_username);
        Task<ResultUnit<string>> InsertQuestionAsked(QuestionAsked i_PreviousQuestionToInsert);

        //delete
        Task InsertQuestionAnswer(QuestionAnswers i_QuestionAskedToInsert);

        Task<ResultUnit<List<UserAnswer>>> GetListOfAnswers(string i_QuestionId);
    }
}

