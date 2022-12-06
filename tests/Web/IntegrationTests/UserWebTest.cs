using Xunit;
using System.Threading.Tasks;
using FluentAssertions;

namespace IntegrationTests;

public class UserWebTest : WebFixture
{
    const string url = "user";
    [Fact]
    public async Task Get()
    {
        var responseMessage = await _client.GetAsync(url);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    [Fact]   
    public async Task Post()
    {
        var responseMessage = await _client.PostAsync(url, null);

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
        var responseMessage = await _client.DeleteAsync(url);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
}
