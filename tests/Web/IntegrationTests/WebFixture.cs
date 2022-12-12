using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Web;
using System;

namespace IntegrationTests;


public class WebFixture : WebApplicationFactory<Program>, IDisposable
{
    
    public HttpClient Client { get; }

    public WebFixture(){
        Client = CreateClient();
    }
    protected override void Dispose(bool disposing)
    {
        
    }
}

[CollectionDefinition("WebContext")]
public class WebFixtireCollection : ICollectionFixture<WebFixture>
{

}