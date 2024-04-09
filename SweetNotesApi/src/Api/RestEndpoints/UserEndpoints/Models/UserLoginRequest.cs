namespace Api.RestEndpoints.UserEndpoints.Models;

public record UserLoginRequest(string EmailAddress, string Password);
