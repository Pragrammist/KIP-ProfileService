using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Web;
using System;

namespace IntegrationTests;

public class WebFixture : WebApplicationFactory<Program>, IDisposable, IClassFixture<WebFixture> 
{
    
    protected HttpClient _client;

    public WebFixture(){
        _client = CreateClient();
    }
    protected override void Dispose(bool disposing)
    {
        _client.Dispose();
        base.Dispose(disposing);
    }
}