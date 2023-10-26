using Microsoft.AspNetCore.Authentication.Cookies;

namespace Api.Auth.Cookie;

public static class AuthCookie
{
    public static void Configure(CookieAuthenticationOptions options)
    {
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
        options.Cookie.HttpOnly = true;
        //options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // TODO: Uncomment when working with HTTPS locally
        options.Cookie.Name = "SweetNotesAuthCookie";
        options.Cookie.IsEssential = true;
        options.Events.OnRedirectToLogin = context =>
        {
            context.Response.Redirect("/");
            return Task.CompletedTask;
        };
    } 
}