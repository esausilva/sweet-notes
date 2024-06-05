namespace Api.Security;

public record CorsSettings(string FrontendOrigin, string? FrontendOriginLocalNetwork);