using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests;

public class ChildWebTest : WebFixture
{
    const string url = "child";
    [Fact]
    public async Task Get()
    {
        var requestArg = "?profileId=someid";
        var responseMessage = await _client.GetAsync(url + requestArg);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]   
    public async Task Post()
    {
        var child = new {
            ProfileId ="someprofileid",
            Age = 12,
            Name = "name",
            Gender = "mail"
        };
        var responseMessage = await _client.PostAsJsonAsync(url, child);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    [Fact]
    public async Task Put()
    {
        var responseMessage = await _client.PutAsync(url, null);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    [Fact]
    public async Task Delete()
    {
        var requestArg = "?id=someid";
        var responseMessage = await _client.DeleteAsync(url + requestArg);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
}
