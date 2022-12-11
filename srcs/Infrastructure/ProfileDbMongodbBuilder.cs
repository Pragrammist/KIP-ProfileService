using System.Security.Cryptography.X509Certificates;
using MongoDB.Driver;
using ProfileService.Core;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;

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
            map.UnmapMember(c => c.WillWatch);
            map.UnmapMember(c => c.Watched);
            map.UnmapMember(c => c.Scored);
            map.UnmapMember(c => c.Childs);
            
            
            map.MapField("_willWatchTables").SetElementName("WillWatch");
            map.MapField("_scoredTables").SetElementName("Scored");
            map.MapField("_watchedTables").SetElementName("Watched");
            map.MapField("_childs").SetElementName("Childs");
        });
        BsonClassMap.RegisterClassMap<ChildProfile>(map =>{
            map.AutoMap();
            

            map.UnmapMember(c => c.Age);
            
            map.MapField("_age").SetElementName("Age");
        });
        BsonClassMap.RegisterClassMap<User>(map => {
            map.AutoMap();
            
        });
    }
}


