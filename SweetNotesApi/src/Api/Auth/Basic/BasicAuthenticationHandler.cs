// using System.Security.Claims;
// using System.Text;
// using System.Text.Encodings.Web;
// using Application.Commands;
// using Application.Commands.UserLogin;
// using Domain.Entities;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.Extensions.Options;
//
// namespace Api.Auth.Basic;
//
// public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
// {
//     private const string AuthorizationHeader = "Authorization";
//     private readonly ICommandRequest<UserLoginCommand, User?> _commandRequest;
//
//     public BasicAuthenticationHandler
//     (
//         IOptionsMonitor<AuthenticationSchemeOptions> options, 
//         ILoggerFactory logger, 
//         UrlEncoder encoder, 
//         ISystemClock clock,
//         ICommandRequest<UserLoginCommand, User?> commandRequest
//     ) : base(options, logger, encoder, clock)
//     {
//         _commandRequest = commandRequest;
//     }
//
//     protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
//     {
//         // var endpoint = Context.GetEndpoint();
//         // if (endpoint?.Metadata?.GetMetadata<IAllowAnonymous>() != null)
//         // {
//         //     return await Task.FromResult(AuthenticateResult.NoResult());
//         // }
//
//         if (Request.Headers.ContainsKey(AuthorizationHeader) is false)
//         {
//             // throw new UnauthorizedException();
//             return await Task.FromResult(AuthenticateResult.Fail("Missing auth key"));
//         }
//         
//         var authHeader = Request.Headers[AuthorizationHeader].ToString();
//         const string basicAuthScheme = $"{BasicAuthenticationDefaults.AuthenticationScheme} ";
//         
//         if (authHeader.StartsWith(basicAuthScheme, StringComparison.OrdinalIgnoreCase) is false)
//         {
//             // throw new UnauthorizedException();
//             return await Task.FromResult(AuthenticateResult.Fail("Authorization header does not start with 'Basic'"));
//         }
//         
//         var authBase64Decoded = Encoding.UTF8.GetString(
//             Convert.FromBase64String(
//                 authHeader.Replace(basicAuthScheme, "", StringComparison.OrdinalIgnoreCase)
//                 )
//         );
//         
//         var authSplit = authBase64Decoded.Split(':', 2);
//
//         if (authSplit.Length != 2)
//         {
//             // throw new UnauthorizedException();
//             return await Task.FromResult(AuthenticateResult.Fail("Invalid auth header format"));
//         }
//         
//         var emailAddress = authSplit[0];
//         var password = authSplit[1];
//         
//         // db check for correct password
//         var command = new UserLoginCommand(emailAddress, password);
//         var user = await _commandRequest.Handle(command, default);
//
//         if (user is null)
//         {
//             // throw new UnauthorizedException();
//             return await Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
//         }
//
//         var claimsIdentity = new ClaimsIdentity(
//             identity: new BasicAuthenticationClient
//             {
//                 AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
//                 IsAuthenticated = true,
//                 Name = emailAddress
//             }, 
//             claims: new[]
//             {
//                 new Claim(ClaimTypes.Email, emailAddress),
//                 new Claim(ClaimTypes.Sid, user.Id.ToString()),
//                 new Claim(ClaimTypes.Role, "User")
//             }, 
//             authenticationType: BasicAuthenticationDefaults.AuthenticationScheme, 
//             nameType: ClaimTypes.Email, 
//             roleType: ClaimTypes.Role
//         );
//         var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
//
//         return await Task.FromResult(
//             AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name))
//         );
//     }
// }