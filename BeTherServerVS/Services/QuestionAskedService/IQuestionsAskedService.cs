using System;
using BeTherServer.Models;
using BeTherServer.Services.Utils;

namespace BeTherServer.Services
{
	public interface IQuestionsAskedService
	{
        Task<ResultUnit<List<QuestionAsked>>> GetUsersPreviousQuestions(string i_username);
        Task<ResultUnit<List<string>>> InsertQuestionAsked(QuestionAsked i_PreviousQuestionToInsert);
    }
}

