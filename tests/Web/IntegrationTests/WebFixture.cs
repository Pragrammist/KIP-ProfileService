using System.Net.Http;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Builder;
using Web;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Appservices;
using Infrastructure;

namespace IntegrationTests;


public class WebFixture : WebApplicationFactory<Program>, IDisposable
{
     private bool disposed = false;
    public HttpClient Client { get; }
    readonly string DB_NAME = "kip_profile_test_db";
    public WebFixture(){
        Environment.SetEnvironmentVariable("DB_NAME", DB_NAME);

        Client = CreateClient();
    }
    protected override void Dispose(bool disposing)
    {
        if (disposed) return;
        if (disposing)
        {
            var mongoDb = Services.GetRequiredService<IMongoClient>();   
            mongoDb.DropDatabase(DB_NAME);
            
        }
        disposed = true;
        base.Dispose();
    }
}

[CollectionDefinition("WebContext")]
public class WebFixtireCollection : ICollectionFixture<WebFixture>
{

}