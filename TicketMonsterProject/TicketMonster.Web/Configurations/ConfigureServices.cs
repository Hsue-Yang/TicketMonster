using TicketMonster.ApplicationCore.Extensions;
using TicketMonster.ApplicationCore.Interfaces.Access;
using TicketMonster.ApplicationCore.Services.Access;
using TicketMonster.ApplicationCore.Interfaces.EventService;
using TicketMonster.ApplicationCore.Interfaces.PerformerService;
using TicketMonster.ApplicationCore.Interfaces.User;
using TicketMonster.ApplicationCore.Services.Lineup;
using TicketMonster.ApplicationCore.Services.User;
using TicketMonster.ApplicationCore.Services;
using TicketMonster.Infrastructure.Data;
using TicketMonster.Web.Services.Cms;
using TicketMonster.Web.Services.Event;
using TicketMonster.Web.Services.Home;
using TicketMonster.Web.Services.Performer;
using TicketMonster.Web.Services;
using static TicketMonster.ApplicationCore.Services.CoravelService;
using TicketMonster.ApplicationCore.Interfaces.PurchaseService;
using TicketMonster.Web.Interfaces;
using System.Configuration;
using TicketMonster.Web.Services.CatchService;

namespace TicketMonster.Web.Configurations;

public static class ConfigureServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<IAccess, Access>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<UserManager>();
        services.AddScoped<EmailSender>();
        services.AddScoped<EmailTest>();
        services.AddScoped<IHomePageViewModelService, RedisCatchHomePageViewModelService>();
        services.AddScoped<HomePageViewModelService>();
        services.AddScoped<IPerformerService, PerformerService>();
        services.AddScoped<IPurchaseService, PurchaseService>();
        services.AddScoped<IPurchaseVMService, PurchaseVMService>();
        services.AddScoped<PurchaseService>();   
        //services.AddScoped<IEventViewModelService,EventViewModelService>();
        services.AddScoped<IEventViewModelService,RedisCatchEventViewModelService>();
        services.AddScoped<EventViewModelService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<PerformerViewModelService>();
        services.AddScoped<PerformerService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<EventService>();
        services.AddScoped<CreateTempOrderRepository, TempOrderRepository>();
        services.AddScoped<CreateOrderRepository, OrderRepository>();
        services.AddScoped<TempOrderRepository>();
        services.AddScoped<OrderRepository>();
        services.AddScoped<TempOrderService>();
        services.AddScoped<CheckOutService>();
        services.AddScoped<CheckOutPageService>();
        services.AddScoped<EcPayService>();
		services.AddScoped<UserService>();
		services.AddScoped<CountOrderTotalMoneyService>();
        return services;
    }
}

