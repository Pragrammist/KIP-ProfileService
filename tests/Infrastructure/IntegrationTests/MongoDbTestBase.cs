using System;
using Infrastructure;
using ProfileService.Core;
using MongoDB.Driver;

namespace IntegrationTests;

public class MongoDbTestBase : IDisposable{

    protected IMongoClient _client;
    protected IMongoDatabase _db;
    protected const string DB_NAME = "kip-test-profile-db";
    protected const string COLECTION_NAME = "profiles";
    protected readonly IMongoCollection<Profile> _repo;

    public MongoDbTestBase(){
        _client = new MongoClient("mongodb://localhost:27017");
        _db = _client.GetDatabase(DB_NAME);
        Build();
        _repo = _db.GetCollection<Profile>(COLECTION_NAME);
    }

    protected readonly InsertOneOptions _insOpt = new InsertOneOptions {};

    protected readonly FilterDefinition<Profile> allFilter = Builders<Profile>.Filter.Empty;

    public void Dispose()
    {
        _client.DropDatabase(DB_NAME);
    }
    void Build(){
        ProfileDbMongodbBuilder mongoBuilder = new ProfileDbMongodbBuilderImpl();
        mongoBuilder.Build();
    }
    
}

