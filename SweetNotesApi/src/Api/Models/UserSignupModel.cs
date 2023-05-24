namespace Api.Models;

public record UserSignupModel
(
    string FirstName,
    string LastName,
    string EmailAddress,
    string Password
);