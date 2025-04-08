using TicketMonster.ApplicationCore.Interfaces.PurchaseService;
using TicketMonster.Web.ViewModels.Purchase;

namespace TicketMonster.Web.Services.Cms
{
    public interface IPurchaseVMService
    {

        Task<PurchasePageViewModel> GetPurchasePageViewModel(int eventId);

        
        Task<string> GetSeatNumList(int eventId);
        Task<string> GetSeatSectionList(int eventId);
        SeatNumsViewModel GetEventSeat(int eventId, string sectionName, string seatNum);
        Task<string> GetTheaterSeat();
    }
}
