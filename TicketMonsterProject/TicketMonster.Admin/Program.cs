using Coravel;
using Coravel.Scheduling.Schedule;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System;
using TicketMonster.Admin.Configurations;
using TicketMonster.Admin.WebApi;
using static TicketMonster.ApplicationCore.Services.CoravelService;

namespace TicketMonster.Admin
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            TicketMonster.Infrastructure.Dependencies.SqlServerConfigureServices(builder.Configuration, builder.Services);
            TicketMonster.Infrastructure.Dependencies.RedisCacheConfigureServices(builder.Configuration, builder.Services);

            builder.Services.AddSwaggerSettings();
            builder.Services.AddJWTSettings(builder.Configuration);
            builder.Services.AddCloudinarySettings(builder.Configuration);
            builder.Services.AddServices();
            builder.Services.AddControllersWithViews();
            builder.Services.AddScheduler();
            builder.Services.AddTransient<ReserveCheck>();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // 先驗證再授權
            app.UseAuthentication();
            app.UseAuthorization();

            app.Services.UseScheduler(schdeuler =>
            {
                schdeuler.Schedule<ReserveCheck>().EverySeconds(10);
                app.Services.CreateScope().ServiceProvider.GetRequiredService<DashboardController>().DailySchedule();
            });

            app.MapControllerRoute(
            name: "Login",
            pattern: "Login",
              new { Controller = "Auth", Action = "Login" }
            );

            app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Dashboard}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
