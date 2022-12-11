using Microsoft.AspNetCore.Mvc;
using Appservices.CreateProfileDtos;
using Appservices.OutputDtos;
using Serilog;

namespace Web.Controllers;


[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ProfileController : ControllerBase
{

    //Сделать для пользователя, профиля и для детского профиля отдельные контроллеры или объединить их в один?
    //сделать все отдельными контроллерами, но сделать создание профиля, 
    //в которое принимается все возоможные данные профиля, но для остального сделать чтобы каждый отвечал за свое

    /// <summary>
    /// Создание профиля. в поля json вводятся данные которые относятся User(потча, пароль и т.д.).
    /// </summary>
    /// <remarks>
    /// Если нужно создать пользователя, которого еще нет в кинопосике, то нужно пользоваться этим методом. 
    /// Т.е. USER НЕ СОЗДАЕТСЯ В КОНТРОЛЛЕРЕ USER, 
    /// как бы не звучало парадоксально, но связано это с тем, чтобы была целостность данных.
    /// userId: айди, который authService создал
    /// </remarks>
    /// <response code="201">создался профиль</response>
    /// <response code="400">Не прошло валидацию</response>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost]
    public ProfileDto Post(CreateProfileDto input)
    {
        return new ProfileDto 
        {
            Id = "generatedid",
            User = new UserDto{
                Email = input.Email,
                Id = input.UserId, 
                Login = input.Login,
                Password = input.Password,                
            }            
        };
    }


    /// <summary>
    /// Возращает полный профиль по id, т.е. тянется вся инфа профиля(user, детские профиля, списки фильмов)
    /// </summary>
    /// <response code="200">нашелся профиль</response>
    /// <response code="404">не нашелся профиль</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public ProfileDto Get(string id)
    {
        var profile = new ProfileDto 
        {
            Id = "generatedid",
            User = new UserDto
            {
                CreatedAt = DateTime.Now,
                Email = "input.Email",
                Id = "input.Id",
                Login = "input.Login",
                Password = "input.Password",
            }            
        };
        Log.Information("FUCKING PROFILE IS {@0}", profile);
        return profile;
    }

    /// <summary>
    /// Не работает сделан, чтобы просто был на будущее. 
    /// Этот метод будет для изменения каких-то данных в профиля
    /// </summary>
    [HttpPut]
    public object Put(){
        return "put";
    }

    /// <summary>
    /// Не работает сделан, чтобы просто был на будущее.
    /// Нужно ли делать удаление профиля, хоть и для админки полезно будет, но только нужно ли это
    /// </summary>
    [HttpDelete]
    public object Delete(){
        return "delete";
    }

}
