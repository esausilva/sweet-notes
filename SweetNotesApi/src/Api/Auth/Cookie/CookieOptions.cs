using Microsoft.AspNetCore.Authentication.Cookies;

namespace Api.Auth.Cookie;

public static class CookieOptions
{
    public static void Configure(CookieAuthenticationOptions options)
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.HttpOnly = false;
        options.Cookie.Name = "SweetNotesCookie";
        options.Cookie.IsEssential = true;
        options.Events.OnSignedIn = context =>
        {
            context.Response.Redirect("/graphql/");
            return Task.CompletedTask;
        };
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Redirect("/");
            return Task.CompletedTask;
        };
    } 
}