using System.IO;
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

    class UserToSend{
        public string Login {get; set; } = null!;
        public string Email {get; set; } = null!;
        public string Password {get; set; } = null!;
    }
    class ChildProfileToSend{
        public string ProfileId {get; set;} =  null!;
        public int Age {get; set;}
        public string Name {get; set;} =  null!;
        public int Gender {get; set;}
    }
    readonly WebFixture _webContext;
    readonly string profileUrlPost = "profile";
    const string url = "child";
    UserToSend FillRandmlyUserField()
    {
        var user = new UserToSend();
        user.Login = Path.GetRandomFileName();
        user.Password = Path.GetRandomFileName();
        user.Email = Path.GetRandomFileName();
        return user;
    }
    async Task<ChildProfileToSend> CreateChild(){
        var child = new ChildProfileToSend{
            ProfileId = await CreateProfile(),
            Age = 12,
            Name = Path.GetRandomFileName(),
            Gender = 0
        };
        return child;
    }
    public ChildWebTest(WebFixture webContext){
        _webContext = webContext;
    }

    [Fact]   
    public async Task Post()
    {
        var child = await CreateChild();
        var responseMessage = await _webContext.Client.PostAsJsonAsync(url, child);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }

    [Fact]
    public async Task Delete()
    {
        var child = await CreateChildProfile();
        var requestArg = $"?profileId={child.ProfileId}&name={child.Name}";
        var responseMessage = await _webContext.Client.DeleteAsync(url + requestArg);

        responseMessage.IsSuccessStatusCode.Should().BeTrue();
    }
    async Task<string> CreateProfile (){ 
        var userToPost = FillRandmlyUserField();
        var postResponseMessage = await _webContext.Client.PostAsJsonAsync(profileUrlPost, userToPost);

        var json = await postResponseMessage?.Content?.ReadAsStringAsync();
        var jsobj = JObject.Parse(json);
        var res  = jsobj["id"].ToString();

        
        return res;
    }
    async Task<ChildProfileToSend> CreateChildProfile(){
        var child = await CreateChild();
        var responseMessage = await _webContext.Client.PostAsJsonAsync(url, child);
        return child;
    }
}
