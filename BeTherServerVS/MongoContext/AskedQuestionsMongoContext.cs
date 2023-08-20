using BeTherServer.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;
using BeTherServer.MongoContext;
using BeTherServer.Services;
using Azure;


namespace BeTherMongoDB.Services;

public class AskedQuestionsMongoContext : BaseMongoContext<QuestionAsked>,IAskedQuestionDBContext
{

    private static readonly string m_collectionName = "AskedQuestions";


    public AskedQuestionsMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings) : base(i_mongoDBSettings, m_collectionName)
    {

    }

    public async Task<List<QuestionAsked>> GetPerviousQuestionsByUser(string i_username)
    {
        FilterDefinition<QuestionAsked> filter = Builders<QuestionAsked>.Filter.Eq("username", i_username);
        List<QuestionAsked> response = await base.GetAllObjectsByFilter(filter);
        return response;
    }

    public async Task InsertNewQuestionAsked(QuestionAsked i_NewQuestion)
    {
        await base.InsertOneObject(i_NewQuestion);
        return;
    }

    //public async Task<List<PreviousQuestions>> GetAsync()
    //{
    //    return await m_previousQuestionsCollection.Find(new BsonDocument()).ToListAsync();
    //}

    //public async Task AddToPreviousQuestionsAsync(string i_id, string i_previousQuestionId)
    //{
    //    FilterDefinition<PreviousQuestions> filter = Builders<PreviousQuestions>.Filter.Eq("Id", i_id);
    //    UpdateDefinition<PreviousQuestions> update = Builders<PreviousQuestions>.Update.AddToSet<string>("questions_items", i_previousQuestionId);
    //    await m_previousQuestionsCollection.UpdateOneAsync(filter, update);
    //    return;
    //}

    //public async Task DeleteAsync(string i_id)
    //{
    //    FilterDefinition<PreviousQuestions> filter = Builders<PreviousQuestions>.Filter.Eq("Id", i_id);
    //    await m_previousQuestionsCollection.DeleteOneAsync(filter);
    //    return;
    //}

}
