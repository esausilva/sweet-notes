using Api.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Api.RestEndpoints.UserEps;

public static class UserLogout
{
    public static async Task GetAsync(HttpContext context, CorsSettings corsSettings)
    {
        await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        context.Response.Redirect(corsSettings.FrontendOrigin);
    }
}