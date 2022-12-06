using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;



[ApiController]
[Route("[controller]")]
public class ProfileController : ControllerBase
{

    //Сделать для пользователя, профиля и для детского профиля отдельные контроллеры или объединить их в один?
    //сделать все отдельными контроллерами, но сделать создание профиля, 
    //в которое принимается все возоможные данные профиля, но для остального сделать чтобы каждый отвечал за свое
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
