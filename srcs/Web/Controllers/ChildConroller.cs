using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ChildController : ControllerBase
{
    [HttpPost]
    public object Post()
    {
        return "post";
    }

    [HttpGet]
    public object Get()
    {
        return "get";
    }

    [HttpPut]
    public object Put(){
        return "put";
    }

    [HttpDelete]
    public object Delete(){
        return "delete";
    }
}