using Microsoft.AspNetCore.Authentication.Cookies;

namespace Api.Auth.Cookie;

public static class AuthCookie
{
    public static void Configure(CookieAuthenticationOptions options)
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.HttpOnly = true;
        options.Cookie.Name = "SweetNotesCookie";
        options.Cookie.IsEssential = true;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Redirect("/");
            return Task.CompletedTask;
        };
    } 
}