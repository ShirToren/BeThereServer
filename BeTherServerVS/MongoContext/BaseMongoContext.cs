using System;
using BeTherServer.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BeTherServer.MongoContext;

public class BaseMongoContext<T>
{
    private MongoClient client;
    private readonly IMongoDatabase database;
    private readonly IMongoCollection<T> m_Collection;

    public BaseMongoContext(IOptions<MongoDBSettings> i_mongoDBSettings, string i_collectionName)
    {
        client = new MongoClient(i_mongoDBSettings.Value.m_connectionURI);
        database = client.GetDatabase(i_mongoDBSettings.Value.m_databaseName);
        m_Collection = database.GetCollection<T>(i_collectionName);
    }

    public IMongoCollection<T> Collection { get { return m_Collection; }}



    public async Task<List<T>> GetAllObjectFromCollection()
    {
        return await m_Collection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task<List<T>> GetAllObjectsByFilter(FilterDefinition<T> filter)
    {
        return await m_Collection.Find(filter).ToListAsync();
    }

    public async Task InsertOneObject(T i_objectToInsrtToCollection)
    {
        await m_Collection.InsertOneAsync(i_objectToInsrtToCollection);
    }


}

