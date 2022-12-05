namespace ProfileService.Core;

//TODO: Exceptions if will needed

public class User
{

    private User() { }
    public User(string id, string login, string email, string password)
    {
        Id = id;
        Login = login;
        Email = email;
        Password = password;
    }
    public string Id { get; set; } = null!;
    public string Login { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreatedAt { get; private set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; }
}
