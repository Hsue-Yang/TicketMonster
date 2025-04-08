using Microsoft.OpenApi.Models;

namespace TicketMonster.Admin.Configurations;

public static class ConfigureSwaggerSettings
{
    public static IServiceCollection AddSwaggerSettings(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "TicketMonster Admin API",
                Description = "CMS...",
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme() 
            {
                Name = "Authorization", 
                In = ParameterLocation.Header, 
                Type = SecuritySchemeType.ApiKey, Scheme = "Bearer", 
                BearerFormat = "JWT", 
                Description = "JWT Authorization header using the Bearer scheme." 
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                { 
                    new OpenApiSecurityScheme { 
                        Reference = new OpenApiReference 
                        { 
                            Type = ReferenceType.SecurityScheme, 
                            Id = "Bearer" 
                        }
                    }, Array.Empty<string>()
                }
            });
        });
        return services;
    }
}