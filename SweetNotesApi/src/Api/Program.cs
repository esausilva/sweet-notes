using Api.DI;
using Api.Middleware;
using Api.RestEndpoints;
using Application.DI;
using Data.DI;
using Microsoft.AspNetCore.Authentication.Cookies;
using CookieOptions = Api.Auth.Cookie.CookieOptions;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.GetApiConfigurations();

var services = builder.Services;
services
    .ConfigureApiDependencies(configuration)
    .ConfigureApplicationDependencies(configuration)
    .ConfigureDataDependencies(configuration)
    .ConfigureGraphQLDependencies(configuration)
    .AddRazorPages();
services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieOptions.Configure);

var app = builder.Build();
app.ConfigureExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapGraphQL()
    .RequireAuthorization();
app.MapGroup("/user")
    .MapUserEndpoint();
app.Run();
