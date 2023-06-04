namespace Api.Models;

public record CreateUserSignup
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }

    private readonly string _password;
    public string Password
    {
        get => _password;
        init => _password = BCrypt.Net.BCrypt.EnhancedHashPassword(value);
    }
}