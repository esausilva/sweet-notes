using Api.Auth.Cookie;
using Api.DI;
using Api.Exceptions;
using Api.Extensions;
using Api.Security;
using Application.DI;
using Data.DI;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.GetApiConfigurations();

var services = builder.Services;
services
    .ConfigureSettings(configuration)
    .ConfigureApiDependencies(configuration)
    .ConfigureApplicationDependencies(configuration)
    .ConfigureDataDependencies(configuration)
    .ConfigureGraphQLDependencies(configuration)
    .AddCors(Cors.Configure(configuration))
    .AddRazorPages();
services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(AuthCookie.Configure());

var app = builder.Build();

app.UseHsts();

if (app.Environment.IsDevelopment())
    app.ApplyMigrations();
// else
//     app.UseHttpsRedirection();

app.UseExceptionHandler(GlobalExceptionHandler.Configure);
app.UseCors(Cors.FrontEndPolicyName);
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();
app.MapGraphQL();
app.ConfigureEndpoints();
app.Run();
