using Microsoft.AspNetCore.Mvc;
using Appservices.CreateChildProfileDtos;
using Appservices.OutputDtos;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ChildController : ControllerBase
{

    ///<summary>
    /// добавление детского профиля в сущетсвующий взрослый профиль 
    ///</summary>
    [HttpPost]
    public ChildProfileDto Post(CreateChildProfileDto child)
    {
        return new ChildProfileDto{
            Age = child.Age,
            Gender = child.Gender.ToString(),
            Name = child.Name,
            Id = child.ProfileId
        };
    }

    ///<summary>
    /// полученение списка детский профилей по id взрослого профиляы
    ///</summary>
    [HttpGet]
    public IEnumerable<ChildProfileDto> Get(string profileId)
    {
        return new []{
            new ChildProfileDto {
                Age = 12,
                Gender = "mail",
                Name = "name"
            },
            new ChildProfileDto {
                Age = 12,
                Gender = "mail",
                Name = "name"
            },
            new ChildProfileDto {
                Age = 12,
                Gender = "mail",
                Name = "name"
            },
            new ChildProfileDto {
                Age = 12,
                Gender = "mail",
                Name = "name"
            }
        };
    }


    ///<summary>
    /// удаление детского профиля по его id
    ///</summary>
    [HttpDelete]
    public object Delete(string profileId, string name){
        return "deleted " + profileId;
    }
}