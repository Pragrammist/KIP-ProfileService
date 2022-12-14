using ProfileService.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

namespace Infrastructure;



public interface ProfileMongodbBuilder
{
    void Build();
}

public class ProfileMongodbBuilderImpl : ProfileMongodbBuilder
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
