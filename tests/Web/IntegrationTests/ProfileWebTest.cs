using System;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http.Json;

namespace IntegrationTests;

public class ProfileWebTest : WebFixture
{
    const string url = "profile";
    [Fact]
    public async Task Get()
    {
        var requestParams = "?id=ewe";
        var responseMessage = await _client.GetAsync(url+requestParams);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    [Fact]   
    public async Task Post()
    {
        var user = new 
        {
            UserId = "",

            Login = "{ get; set; } = null!;",
            Email = "ew",
            Password = "ewe",
            CreatedAt = DateTime.Now
        };
        var responseMessage = await _client.PostAsJsonAsync(url, user);

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
