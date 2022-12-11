using ProfileService.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using Appservices.OutputDtos;

namespace Infrastructure;



public interface ProfileDbMongodbBuilder
{
    void Build();
}

public class ProfileDbMongodbBuilderImpl : ProfileDbMongodbBuilder
{
    public void Build()
    {
        BsonMemberMap SetStringId<T>(BsonClassMap<T> map) => map.IdMemberMap.SetSerializer(new StringSerializer(BsonType.ObjectId)).SetIdGenerator(StringObjectIdGenerator.Instance);

        BsonClassMap.RegisterClassMap<Profile>(map => {
            map.AutoMap();
            SetStringId(map);
        });
    }
}
