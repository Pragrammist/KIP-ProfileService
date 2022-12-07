using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    ///<summary>
    /// не работает. зна будущее
    /// вдруг нужно будет создать пользователя без создания объекта профиля
    ///</summary>
    [HttpPost]
    public object Post()
    {
        return "post";
    }

    ///<summary>
    /// не работает. на будущее
    ///</summary>
    [HttpGet]
    public object Get()
    {
        return "get";
    }
    ///<summary>
    /// не работает. на будущее
    /// думаю будет нужен во время разработки кинопоиска
    ///для изменения юзера
    ///</summary>
    [HttpPut]
    public object Put(){
        return "put";
    }

    ///<summary>
    /// не работает. на будущее
    /// для админки может нужен будетв
    ///</summary>
    [HttpDelete]
    public object Delete(){
        return "delete";
    }
}
