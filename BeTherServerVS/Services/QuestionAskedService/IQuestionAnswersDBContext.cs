using System;
using BeTherServer.Models;

namespace BeTherServer.Services;

public interface IQuestionAnswersDBContext
{
    Task<QuestionAnswers> GetAnswersByQuestionId(string i_QuestionId);


    Task InsertNewQuestionAsked(QuestionAnswers i_NewAnnswer);
}

