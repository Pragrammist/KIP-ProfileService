using Web.Services;

namespace Web.Middlewares;

public class CreateChildProfileMetricsMiddleware{
    private readonly RequestDelegate _request;
    public CreateChildProfileMetricsMiddleware(RequestDelegate request)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
    }
    public async Task Invoke(HttpContext httpContext, ChildProfileMetrics metrics)
    {
        var path = httpContext.Request.Path;
        var reqType = httpContext.Request.Method;

        if(! (path == "/child" && reqType == "POST"))//creating childprofile
        {
            await _request.Invoke(httpContext);
            return;
        }   

        try
        {
            await _request.Invoke(httpContext);
            metrics.IncCreateProfileSuccefulGrpc();
        }
        catch(Exception){
            metrics.IncCreateProfileFailureGrpc();
            throw;
        }
    }
}