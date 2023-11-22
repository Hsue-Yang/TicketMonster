using TicketMonster.Admin.Interface;

namespace TicketMonster.Admin.Repository;

public class DashboardRepo : BaseRepo, IDashboardRepo
{
    private readonly ICustomerRepo customerRepo;
    private readonly IEventRepo eventRepo;

    public DashboardRepo(IConfiguration configuration, ICustomerRepo customerRepo, IEventRepo eventRepo) : base(configuration)
    {
        this.customerRepo = customerRepo;
        this.eventRepo = eventRepo;
    }

    public async Task<int> GetCustomersCount()
    {
        var result = await customerRepo.GetAllCustomers();
        return result.Count();
    }

    public async Task<int> GetEventsCount()
    {
        var result = await eventRepo.GetAllEvents();
        return result.Count();
    }

    public async Task<IEnumerable<dynamic>> GetEarningsByAnnual()
    {
        var sql = "select YEAR(OrderDate)[Year], SUM(BillingAmount)[EarningsByAnnual] from Orders group by YEAR(OrderDate)";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetEarningsByMonthly()
    {
        var sql = "select YEAR(OrderDate)[Year], MONTH(OrderDate)[Month], SUM(BillingAmount)[EarningsByMonthly] from Orders group by YEAR(OrderDate), MONTH(OrderDate) order by Year";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetCustomerGrowth()
    {
        var sql = "select YEAR(CreateTime)[Year], MONTH(CreateTime)[Month], COUNT(*)[NewUsersCount] from Customers group by YEAR(CreateTime), MONTH(CreateTime)";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetOrderDesire()
    {
        var sql = "select YEAR(OrderDate)[Year], MONTH(OrderDate)[Month], COUNT(*)[OrderDesire] from Orders group by YEAR(OrderDate), MONTH(OrderDate) order by Year";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetPerMonthCategoryCount()
    {
        var sql = "SELECT DATEPART(MONTH, EventDate) AS Month, CategoryID, COUNT(*) AS Count FROM Events\r\nGROUP BY DATEPART(MONTH, EventDate),CategoryID\r\n ORDER BY Month";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetSubCategoryCountInEvent()
    {
        var sql = "SELECT  S.SubCategoryName,e.SubCategoryID,Count(e.SubCategoryID) as SubCategoryCount\r\nFROM Events e\r\nINNER JOIN SubCategory S ON S.ID = E.SubCategoryID\r\ngroup by e.SubCategoryID, S.SubCategoryName";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetPopularEventTop5()
    {
        var sql = "Select  TOP 5\r\n\to.EventName,COUNT(o.EventName) as EventPopular\r\nfrom orders o \r\ngroup by o.EventName\r\n order by EventPopular DESC ";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetEventsPerMonth()
    {
        var sql = "SELECT count(*) as eventNum , month(EventDate) as EventMonth  FROM Events\r\ngroup by MONTH(EventDate)";
        return await QueryAsync<dynamic>(sql);

    }

    public async Task<IEnumerable<dynamic>> GetVenueEventsNum()
    {
        var sql = "Select\tCOUNT(e.VenueID) as eventNum, v.VenueName FROM events e\r\nINNER JOIN Venues v ON v.ID = e.VenueID\r\nGROUP BY v.VenueName";
        return await QueryAsync<dynamic>(sql);

	}

    public async Task<IEnumerable<dynamic>> GetPopularVenueTop3()
    {
        var sql = "Select  TOP 3 \r\no.VenueName,\r\nCOUNT(o.VenueName) as VenuePopular \r\nfrom orders o \r\ngroup by o.VenueName \r\norder by VenuePopular DESC ";
        return await QueryAsync<dynamic>(sql);
    }
}
