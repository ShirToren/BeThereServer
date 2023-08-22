using System;
using BeTherServer.Models;
using BeTherServer.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

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

    public async Task InsertNewQuestionAnswers(QuestionAnswers i_NewAnnswer)
    {
        await base.InsertOneObject(i_NewAnnswer);
        return;
    }
    public async Task CreateNewQuestionAnswersItem(string i_QuestionId)
    {
        QuestionAnswers questionAnswers = new QuestionAnswers();
        questionAnswers.questionId = i_QuestionId;
        questionAnswers.userAnswer = new List<UserAnswer>();
        await base.InsertOneObject(questionAnswers);
        return;
    }
    public async Task InsertAnswerByQuestionId(UserAnswer I_Answer)
    {

        FilterDefinition<QuestionAnswers> filter = Builders<QuestionAnswers>.Filter.Eq("questionId", I_Answer.questionId);
        QuestionAnswers response = await base.FindObjectByFilter(filter);
        if (response != null)
        {
            var responseObject = response.ToBsonDocument();
            var list = responseObject.GetValue("userAnswer").AsBsonArray;
            list.Add(new BsonDocument{
                    {"username", I_Answer.username},
                    {"text", I_Answer.text},
                    {"questionId", I_Answer.questionId},
                    {"chatRoomId", I_Answer.chatRoomId},
                    {"Timestamp", I_Answer.Timestamp}
                });
            UpdateDefinition<QuestionAnswers> update = Builders<QuestionAnswers>.Update.Set("userAnswer", list);
            base.Collection.UpdateOne(filter, update);
        }
        else
        {
           // Console.WriteLine("Document not found.");
        }

       return;
    }
}
