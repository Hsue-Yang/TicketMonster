using TicketMonster.Web.ViewModels.Home;

namespace TicketMonster.Web.Interfaces
{
    public interface IHomePageViewModelService
    {
        Task<HomePageViewModel> GetHomepageViewModel();
    }
}