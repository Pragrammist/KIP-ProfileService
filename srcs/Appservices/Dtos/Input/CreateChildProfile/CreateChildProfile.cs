namespace Appservices.CreateChildProfileDtos;

public class CreateChildProfile
{
    public string ProfileId { get; set; } = null!;
    public int Age { get; set; } 
    public string Name { get; set; } = null!;
    public string Gender { get; set; }  = null!;
}