using Microsoft.AspNetCore.Mvc;
using Appservices.CreateProfileDtos;
using Appservices.OutputDtos;

namespace Web.Controllers;



[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{

    //Сделать для пользователя, профиля и для детского профиля отдельные контроллеры или объединить их в один?
    //сделать все отдельными контроллерами, но сделать создание профиля, 
    //в которое принимается все возоможные данные профиля, но для остального сделать чтобы каждый отвечал за свое
    [HttpPost]
    public ProfileDto Post(CreateProfileDto input)
    {
        return new ProfileDto 
        {
            Id = "generatedid",
            User = new UserDto{
                CreatedAt = input.CreatedAt,
                Email = input.Email,
                Id = input.UserId, 
                Login = input.Login,
                Password = input.Password,                
            }            
        };
    }

    [HttpGet]
    public ProfileDto Get(string id)
    {
        return new ProfileDto 
        {
            Id = "generatedid",
            User = new UserDto{
                CreatedAt = DateTime.Now,
                Email = "input.Email",
                Id = "input.Id", 
                Login = "input.Login",
                Password = "input.Password",                
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
