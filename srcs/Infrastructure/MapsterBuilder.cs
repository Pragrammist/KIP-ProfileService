using ProfileService.Core;
using Mapster;
using Appservices.CreateProfileDtos;
using Appservices.CreateChildProfileDtos;

namespace Infrastructure;

public class MapsterBuilder 
{
    
    public static void ConfigureMapster(){
        TypeAdapterConfig<CreateProfileDto, Profile>.NewConfig().ConstructUsing(p => new Profile(new User(p.Login, p.Email, p.Password)));
        TypeAdapterConfig<CreateChildProfileDto, ChildProfile>.NewConfig().ConstructUsing(ch => new ChildProfile(ch.Age, ch.Name, (Gender)ch.Gender));
    }   
}
