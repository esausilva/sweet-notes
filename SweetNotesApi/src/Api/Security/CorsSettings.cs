namespace Api.Security;

public record CorsSettings
{
    public string FrontendOrigin { get; init; }
    public string? FrontendOriginLocalNetwork { get; init; }
}