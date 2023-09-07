using System;
using BeTherServer.Models;

namespace BeTherServer.Services;

public interface IQuestionAnswersDBContext
{
    Task<QuestionAnswers> GetAnswersByQuestionId(string i_QuestionId);


    Task InsertNewQuestionAnswers(QuestionAnswers i_NewAnnswer);
    Task CreateNewQuestionAnswersItem(string i_QuestionId);
    Task InsertAnswerByQuestionId(UserAnswer I_Answer);
}

