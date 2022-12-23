using System.Reflection;
using Serilog;
using Appservices;
using Infrastructure;
using GrpcProfileService;
using Web.Services;
using Web.Middlewares;
using Web.GrpcInterceptors;
using Prometheus;

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
        MapsterBuilder.ConfigureMapster();
        var builder = WebApplication.CreateBuilder(args);
        
        //var configuration = builder.Configuration;
        var logstashUrl = builder.Configuration["LOGSTASH_URL"] ?? "http://localhost:8080";
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File(@"Logs\Log.txt")
            .WriteTo.Console()
            .WriteTo.Http(logstashUrl, queueLimitBytes: null)
            .CreateLogger();
        

        builder.Host.UseSerilog();

        builder.Services.AddControllers();
        builder.Services.AddGrpc(opt =>{
            opt.Interceptors.Add<CreateProfileMetricsInterceptor>();
        });
        builder.Services.AddGrpcReflection();
        
        builder.Services.AddSingleton<ProfileMetrics>();
        builder.Services.AddSingleton<ChildProfileMetrics>();

        BuildServicesNotFromWeb(builder.Services, builder.Configuration);
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options => {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), includeControllerXmlComments: true);
        });

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            
        }
        app.UseRouting();
        app.UseHttpMetrics(options => options.ReduceStatusCodeCardinality());
        app.UseGrpcMetrics();
        app.UseSwagger();
        app.UseSwaggerUI(options => {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = string.Empty;
        });
        app.UseMiddleware<CreateChildProfileMetricsMiddleware>();
        app.UseAuthorization();
        app.MapGrpcService<ProfileGrpcService>();
        app.MapControllers();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
        });
        app.Run();
    }
    static public void BuildServicesNotFromWeb(IServiceCollection services, IConfiguration configuration)
    {
        MapsterBuilder.ConfigureMapster();
        var connection = configuration["MONGODB_CONNECTION_STRING"] ?? "mongodb://localhost:27017";
        var dbName = configuration["DB_NAME"] ?? "kip_profile_db";
        var collections = configuration["COLLECTION_NAME"] ?? "profiles";
        services.AddMongoDb(connection, dbName, collections);
        services.AddScoped<ProfileRepository, ProfileRepositoryImpl>();
        services.AddScoped<ProfileInteractor>();
    }
}



