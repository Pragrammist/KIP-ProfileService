using System;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace IntegrationTests;

[Collection("WebContext")]
public class ProfileWebTest
{
    readonly WebFixture _webContext;
    public ProfileWebTest(WebFixture webContext){
        _webContext = webContext;
    }
    const string url = "profile";
    object userToPost => new 
        {
            Login = "{ get; set; } = null!;",
            Email = "ew",
            Password = "ewe",
        };
    
    [Fact]
    public async Task Get()
    {
        var postResponseMessage = await _webContext.Client.PostAsJsonAsync(url, userToPost);
        string id = await GetUserId(postResponseMessage);        ;
        var requestParams = $"?id={id}";
        var responseMessage = await _webContext.Client.GetAsync(url+requestParams);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    [Fact]   
    public async Task Post()
    {
        var responseMessage = await _webContext.Client.PostAsJsonAsync(url, userToPost);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    async Task<string> GetUserId (HttpResponseMessage response){ 
        var json = await response?.Content?.ReadAsStringAsync() ?? "{}";
        var jsobj = JObject.Parse(json);
        var res = jsobj["id"].ToString();
        return res;
    }
    
}

