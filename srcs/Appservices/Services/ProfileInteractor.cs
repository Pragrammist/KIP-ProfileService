using Appservices.CreateChildProfileDtos;
using Appservices.CreateProfileDtos;
using Appservices.OutputDtos;
using Appservices.Exceptions;
namespace Appservices;

public class ProfileInteractor
{
    ProfileRepository _repo;
    public ProfileInteractor(ProfileRepository repo){
        _repo = repo;
    }

    public async Task<ProfileDto> Create(CreateProfileDto profileInfoDto, CancellationToken token = default){
        if(await _repo.CountBy(profileInfoDto.Email, profileInfoDto.Login) > 0)
            throw new UserAlreadyExistsException();


        var res =  await _repo.CreateProfile(profileInfoDto, token);
        
        return res;
    }
    

    public async Task<bool> AddChildProfile(CreateChildProfileDto childInfoDto, CancellationToken token = default){
        var res = await _repo.AddChildProfile(childInfoDto, token);

        return res;
    }
    public async Task<bool> RemoveChildProfile(string profileId, string name, CancellationToken token = default){
        var res = await _repo.RemoveChildProfile(profileId, name, token);
        
        return res;
    }
    public async Task<ProfileDto> GetProfile(string profileId, CancellationToken token = default){
        var res = await _repo.FetchProfile(profileId);

        return res;
    }
    

}