using TicketMonster.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace TicketMonster.Infrastructure;

public static class Dependencies
{
    public static void SqlServerConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddDbContext<TicketMonsterContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("TicketMonsterConnection")));    
    }

    public static void RedisCacheConfigureServices(IConfiguration configuration, IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("RedisConnectionString");
        });
        services.AddSingleton<IDatabase>(provider =>
        {
            return ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnectionString")).GetDatabase();
        });
    }
}