namespace Application.Commands.UserUpdatePassword;

public record UserUpdatePasswordCommand(string EmailAddress, string CurrentPassword, string NewPassword);