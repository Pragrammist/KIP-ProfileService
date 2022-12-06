namespace Appservices.OutputDtos;

public class ProfileDto
{
    public string Id {get; set;} = null!;

    public UserDto User {get; set;} = null!;

    public IList<ChildProfile> Childs {get; set;} = new List<ChildProfile>();

    public IList<string> Watched {get; set;} = new List<string>();

    public IList<string> WhillWatch {get; set;} = new List<string>();

    public IList<string> Scored {get; set;} = new List<string>();
}


