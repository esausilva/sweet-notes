namespace Api.Auth.Claims.Configuration;

public record UserClaims
(
    int UserId,
    string Name,
    string EmailAddress,
    string Role
);