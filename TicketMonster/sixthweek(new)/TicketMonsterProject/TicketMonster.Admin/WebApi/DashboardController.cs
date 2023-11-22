using Coravel.Scheduling.Schedule.Interfaces;
using TicketMonster.Admin.Interface;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace TicketMonster.Admin.WebApi;

public class DashboardController : BaseApiController
{
    private readonly IDashboardRepo dashboardRepo;
    private readonly IScheduler scheduler;
    private readonly IDatabase redis;

    public DashboardController(IDashboardRepo dashboardRepo, IServiceProvider serviceProvider, IDatabase redis)
    {
        this.dashboardRepo = dashboardRepo;
        scheduler = serviceProvider.GetRequiredService<IScheduler>();
        this.redis = redis;
    }

    [HttpHead]
    public void DailySchedule()
    {
        scheduler.ScheduleAsync(async () =>
        {
            var earningsByMonthly = await dashboardRepo.GetEarningsByMonthly();
            var earningsByAnnual = await dashboardRepo.GetEarningsByAnnual();
            await redis.StringSetAsync("GetEarningsByMonthly", string.Join("", earningsByMonthly.Where(x => x.Year == DateTime.Now.Year && x.Month == DateTime.Now.Month).Select(x => x.EarningsByMonthly)));
            await redis.StringSetAsync("GetEarningsByAnnual", string.Join("", earningsByAnnual.Where(x => x.Year == DateTime.Now.Year).Select(x => x.EarningsByAnnual)));
            await redis.StringSetAsync("GetUsersCount", await dashboardRepo.GetCustomersCount());
            await redis.StringSetAsync("GetEventsCount", await dashboardRepo.GetEventsCount());
        }).DailyAt(0, 0);
    }

    [HttpGet]
    public async Task<IActionResult> GetDashboardCards()
    {
        decimal earningsByMonthly = Convert.ToDecimal(await redis.StringGetAsync("GetEarningsByMonthly"));
        decimal earningsByAnnual = Convert.ToDecimal(await redis.StringGetAsync("GetEarningsByAnnual"));
        int usersCount = int.Parse(await redis.StringGetAsync("GetUsersCount"));
        int eventsCount = int.Parse(await redis.StringGetAsync("GetEventsCount"));
        var result = new { earningsByMonthly, earningsByAnnual, usersCount, eventsCount };
        return Ok(result);
    }

    #region
    //[HttpGet]
    //public async Task<IActionResult> GetUsersCount()
    //{
    //    var result = await dashboardRepo.GetCustomersCount();
    //    return Ok(result);
    //}

    //[HttpGet]
    //public async Task<IActionResult> GetEventsCount()
    //{
    //    var result = await dashboardRepo.GetEventsCount();
    //    return Ok(result);
    //}
    #endregion

    [HttpGet]
    public async Task<IActionResult> GetEarningsByAnnual()
    {
        var result = await dashboardRepo.GetEarningsByAnnual();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetEarningsByMonthly()
    {
        var result = await dashboardRepo.GetEarningsByMonthly();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetUserGrowth()
    {
        var result = await dashboardRepo.GetCustomerGrowth();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetOrderDesire()
    {
        var result = await dashboardRepo.GetOrderDesire();
        return Ok(result);
    }


    [HttpGet]
    public async Task<IActionResult> GetSubCategoryCountInEvent()
    {
        var result = await dashboardRepo.GetSubCategoryCountInEvent();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPopularEventTop5()
    {
        var result = await dashboardRepo.GetPopularEventTop5();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPerMonthCategoryCount()
    {
        var result = await dashboardRepo.GetPerMonthCategoryCount();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetEventsPerMonth()
    {
        var result = await dashboardRepo.GetEventsPerMonth();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetVenueEventsNum()
    {
        var result = await dashboardRepo.GetVenueEventsNum();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetPopularVenueTop3()
    {
        var result = await dashboardRepo.GetPopularVenueTop3();
        return Ok(result);
    }
}