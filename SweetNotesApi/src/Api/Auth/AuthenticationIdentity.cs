using System.Security.Principal;

namespace Api.Auth;

public sealed class AuthenticationIdentity : IIdentity
{
    public string? AuthenticationType { get; init; }
    public bool IsAuthenticated { get; init; }
    public string? Name { get; init; }
}