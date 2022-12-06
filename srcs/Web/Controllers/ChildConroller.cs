using Microsoft.AspNetCore.Mvc;
using Appservices.CreateChildProfileDtos;
using Appservices.OutputDtos;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ChildController : ControllerBase
{
    [HttpPost]
    public ChildProfile Post(CreateChildProfile child)
    {
        return new ChildProfile{
            Age = child.Age,
            Gender = child.Gender,
            Name = child.Name,
            Id = child.ProfileId
        };
    }

    [HttpGet]
    public IEnumerable<ChildProfile> Get(string profileId)
    {
        return new []{
            new ChildProfile {
                Age = 12,
                Gender = "mail",
                Name = "name"
            },
            new ChildProfile {
                Age = 12,
                Gender = "mail",
                Name = "name"
            },
            new ChildProfile {
                Age = 12,
                Gender = "mail",
                Name = "name"
            },
            new ChildProfile {
                Age = 12,
                Gender = "mail",
                Name = "name"
            }
        };
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