using System.Security.Cryptography.X509Certificates;
using Xunit;
using System.Threading.Tasks;
using FluentAssertions;
using System.Net.Http.Json;
using Newtonsoft.Json.Linq;

namespace IntegrationTests;

[Collection("WebContext")]
public class ChildWebTest
{
    readonly WebFixture _webContext;
    object userToPost => new 
        {
            Login = "{ get; set; } = null!;",
            Email = "ew",
            Password = "ewe",
        };
    
    public ChildWebTest(WebFixture webContext){
        _webContext = webContext;
    }
    const string url = "child";

    [Fact]   
    public async Task Post()
    {
        var child = new {
            ProfileId = await CreateProfile(),
            Age = 12,
            Name = "name",
            Gender = 0
        };
        var responseMessage = await _webContext.Client.PostAsJsonAsync(url, child);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Delete()
    {
        var (profileId,name) = await CreateChildProfile();
        var requestArg = $"?profileId={profileId}&name={name}";
        var responseMessage = await _webContext.Client.DeleteAsync(url + requestArg);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    async Task<string> CreateProfile (){ 
        var profileUrlPost = "profile";
        var postResponseMessage = await _webContext.Client.PostAsJsonAsync(profileUrlPost, userToPost);
        var json = await postResponseMessage?.Content?.ReadAsStringAsync() ?? "{}";
        var jsobj = JObject.Parse(json);
        var res  = jsobj["id"].ToString();
        return res;
    }
    async Task<(string, string)> CreateChildProfile(){
        var profileName = "name";
        var profileId = await CreateProfile();
        var child = new {
            ProfileId = profileId,
            Age = 12,
            Name = profileName,
            Gender = 0
        };
        var responseMessage = await _webContext.Client.PostAsJsonAsync(url, child);
        

        return (profileId,profileName);
    }
}
