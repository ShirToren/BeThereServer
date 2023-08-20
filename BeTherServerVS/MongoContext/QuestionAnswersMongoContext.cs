using System;
using BeTherServer.Models;
using BeTherServer.Services;
using Microsoft.Extensions.Options;

namespace BeTherServer.MongoContext;

public class QuestionAnswersMongoContext : BaseMongoContext<QuestionAnswers>, IQuestionAnswersDBContext
{

    private static readonly string m_collectionName = "QuestionAnswers";


    public QuestionAnswersMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings) : base(i_mongoDBSettings, m_collectionName)
    {
    }

    public async Task<QuestionAnswers> GetAnswersByQuestionId(string i_QuestionId)
    {
        QuestionAnswers response = await  base.Collection.Find(x => x.questionId == i_QuestionId).FirstOrDefaultAsync();
        return response;
    }

    public async Task InsertNewQuestionAsked(QuestionAnswers i_NewAnnswer)
    {
        await base.InsertOneObject(i_NewAnnswer);
        return;
    }
}
