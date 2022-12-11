using Xunit;
using ProfileService.Core;
using System.Threading.Tasks;
using FluentAssertions;

namespace IntegrationTests;

public class ConnectionMongoTest : MongoDbTestBase
{
    [Fact]
    public async Task CreateProfileTest()
    {
        var firstCount = await _repo.CountDocumentsAsync(allFilter);
        var user = new User("lgoin", "email", "password228hash");
        var profile = new Profile(user);

        
        await _repo.InsertOneAsync(profile);


        var count = await _repo.CountDocumentsAsync(allFilter);
        count.Should().BeGreaterThan(firstCount);
    }
    
}

