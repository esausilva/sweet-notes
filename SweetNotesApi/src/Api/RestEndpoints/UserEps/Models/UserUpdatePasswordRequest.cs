namespace Api.RestEndpoints.UserEps.Models;

public record UserUpdatePasswordRequest(string CurrentPassword, string NewPassword);