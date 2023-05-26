namespace Application.Commands;

public record CreateUserSignupCommand
(
    string FirstName,
    string LastName,
    string EmailAddress,
    string Password
);