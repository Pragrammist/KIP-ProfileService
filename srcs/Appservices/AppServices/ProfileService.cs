using Appservices.CreateProfileDtos;
using Appservices.CreateChildProfileDtos;
using Appservices.OutputDtos;
using MongoDB.Driver;
using ProfileService.Core;


using Mapster;

namespace Appservices;


public interface ProfileRepository 
{
    Task<bool> AddChildProfile(CreateChildProfileDto profileInfo, CancellationToken token = default);
    
    Task<bool> RemoveChildProfile(string profileId, string name, CancellationToken token = default);
    
    Task<ProfileDto> CreateProfile(CreateProfileDto profileData, CancellationToken token = default);
    
    Task<ProfileDto> FetchProfile(string id, CancellationToken token = default);
    
}