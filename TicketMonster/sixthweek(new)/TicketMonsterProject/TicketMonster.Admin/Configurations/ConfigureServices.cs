using TicketMonster.Admin.Filters;
using TicketMonster.Admin.Interface;
using TicketMonster.Admin.Repository;
using TicketMonster.Admin.Scheduler;
using TicketMonster.Admin.WebApi;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.Infrastructure.Data;

namespace TicketMonster.Admin.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<BlockTokenScheduler>();
        services.AddScoped<CustomApiExceptionServiceFilter>();
        services.AddScoped<AdminAuthorize>();
        services.AddScoped<IEventRepo, EventRepo>();
        services.AddScoped<IDashboardRepo, DashboardRepo>();
        services.AddScoped<ICustomerRepo, CustomerRepo>();
        services.AddScoped<DashboardController>();

        return services;
    }
}