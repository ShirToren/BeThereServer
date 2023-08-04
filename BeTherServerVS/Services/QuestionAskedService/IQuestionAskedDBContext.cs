using System;
using BeTherServer.Models;

namespace BeTherServer.Services
{
	public interface IQuestionAskedDBContext
	{
        Task<List<QuestionAsked>> GetPerviousQuestionsByUser(string i_username);
        Task InsertNewQuestionAsked(QuestionAsked i_objectToInsrtToCollection);

    }
}

