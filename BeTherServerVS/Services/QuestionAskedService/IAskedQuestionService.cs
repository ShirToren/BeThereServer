using System;
using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services
{
	public interface IAskedQuestionService
	{
        Task<ResultUnit<Dictionary<string, Tuple<QuestionAsked, QuestionAnswers>>>> GetUsersPreviousQuestionsAndAnswers(string i_username);
        Task<ResultUnit<long>> InsertQuestionAsked(QuestionAsked i_PreviousQuestionToInsert);

        //delete
        Task InsertQuestionAnswer(QuestionAnswers i_QuestionAskedToInsert);
    }
}

