using TicketMonster.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
TicketMonster.Infrastructure.Dependencies.SqlServerConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddDistributedMemoryCache();
TicketMonster.Infrastructure.Dependencies.RedisCacheConfigureServices(builder.Configuration, builder.Services);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddHttpContextAccessor();
builder.Services.AddServices();
builder.Services.AddSwaggerGen();
builder.Services.AddCookieSettings();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithRedirects("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Purchase",
    pattern: "Purchase/{id?}",
    defaults: new { controller = "Purchase", action = "Purchase" }
    );

app.MapControllerRoute(
    name: "CheckOut",
    pattern: "CheckOut/{id?}",
    defaults: new { controller = "CheckOutPage", action = "CheckOutPageView" }
    );

app.MapControllerRoute(
    name: "Performer",
    pattern: "Performer/{id?}/{start?}/{end?}",
    defaults: new { controller = "Performer", action = "PerformerView" }
);

app.MapControllerRoute(
    name: "Category",
    pattern: "Category/{id?}/{subid?}",
    defaults: new { controller = "Lineup", action = "LineupView" }
);

app.MapControllerRoute(
    name: "Search",
    pattern: "Search",
    defaults: new { controller = "Lineup", action = "LineupViewBySearch" }
);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HomePage}/{action=HomePageView}/{id?}");

app.Run();