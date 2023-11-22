using Microsoft.AspNetCore.Authentication.Cookies;

namespace TicketMonster.Web.Configurations;

public static class ConfigureCookieSettings
{
    public static IServiceCollection AddCookieSettings(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = new PathString("/Access/SignIn/");
                options.LogoutPath = new PathString("/Access/Logout/");
                options.AccessDeniedPath = new PathString("/Access/AccessDenied/");
                options.ExpireTimeSpan = TimeSpan.FromDays(30);
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.HttpOnly = true;
            });
        return services;
    }
}
