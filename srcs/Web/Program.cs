using System.Reflection;
using Serilog;
using Appservices.OutputDtos;


namespace Web;

public class Program{

    
    public static void Main(string[] args)
    {
        try{
            AppBuild(args);
        }
        catch(Exception ex){
            Log.Fatal(ex, "Application terminated unexpectedly");
        }
        finally{
            Log.CloseAndFlush();
        }

    }
    static void AppBuild(string[] args){
        var builder = WebApplication.CreateBuilder(args);
        
        //var configuration = builder.Configuration;
        //var logstashUrl = configuration["LOGSTASH_URL"]; //http://localhost:8080
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.Http("http://localhost:8080", queueLimitBytes: null)
            .CreateLogger();
        

        builder.Host.UseSerilog();

        builder.Services.AddControllers();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
        });

        var app = builder.Build();

        
        if (app.Environment.IsDevelopment())
        {
            //
        }

        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}



