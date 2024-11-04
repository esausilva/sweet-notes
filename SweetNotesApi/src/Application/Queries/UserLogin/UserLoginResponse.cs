namespace Application.Queries.UserLogin;

public record UserLoginResponse
(
    int Id,
    string Password,
    string FirstName,
    string LastName
);