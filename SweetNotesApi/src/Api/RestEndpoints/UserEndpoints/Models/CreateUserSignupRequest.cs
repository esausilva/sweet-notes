namespace Api.RestEndpoints.UserEndpoints.Models;

public record CreateUserSignupRequest
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string EmailAddress { get; init; }

    private readonly string _password;
    public string Password
    {
        get => _password;
        init => _password = string.IsNullOrWhiteSpace(value) 
            ? value 
            : BCrypt.Net.BCrypt.EnhancedHashPassword(value);
    }
}