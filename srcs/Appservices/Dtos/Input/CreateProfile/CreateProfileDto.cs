namespace Appservices.CreateProfileDtos;

public class CreateProfileDto
{
    public string UserId { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; private set; }
}

