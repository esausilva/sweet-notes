namespace Api.Models;

public record UserSignupModel
{
    private readonly string _password;
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }

    public string Password
    {
        get => _password;
        init => _password = BCrypt.Net.BCrypt.EnhancedHashPassword(value);
    }
}