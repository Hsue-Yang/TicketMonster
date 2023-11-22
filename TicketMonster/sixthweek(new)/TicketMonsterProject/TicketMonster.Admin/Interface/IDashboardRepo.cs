namespace TicketMonster.Admin.Interface;

public interface IDashboardRepo
{
    Task<int> GetCustomersCount();
    Task<int> GetEventsCount();
    Task<IEnumerable<dynamic>> GetEarningsByAnnual();
    Task<IEnumerable<dynamic>> GetEarningsByMonthly();
    Task<IEnumerable<dynamic>> GetCustomerGrowth();
    Task<IEnumerable<dynamic>> GetOrderDesire();
    Task<IEnumerable<dynamic>> GetSubCategoryCountInEvent();
	Task<IEnumerable<dynamic>> GetPopularEventTop5();
    Task<IEnumerable<dynamic>> GetPerMonthCategoryCount();
	Task<IEnumerable<dynamic>> GetEventsPerMonth();
    Task<IEnumerable<dynamic>> GetPopularVenueTop3();
	Task<IEnumerable<dynamic>> GetVenueEventsNum();

}
