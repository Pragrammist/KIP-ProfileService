using Microsoft.AspNetCore.Mvc;
using Appservices.CreateChildProfileDtos;
using Appservices.OutputDtos;
using Appservices;

namespace Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ChildController : ControllerBase
{

    ProfileInteractor _profile;
    public ChildController(ProfileInteractor profile){
        _profile = profile;
    }
    ///<summary>
    /// добавление детского профиля в сущетсвующий взрослый профиль 
    ///</summary>
    [HttpPost]
    public async Task <bool> Post(CreateChildProfileDto child){
        return await _profile.AddChildProfile(child);
    }



    ///<summary>
    /// удаление детского профиля по его id
    ///</summary>
    [HttpDelete]
    public async Task<bool> Delete(string profileId, string name){
        return await _profile.RemoveChildProfile(profileId, name);
    }
}