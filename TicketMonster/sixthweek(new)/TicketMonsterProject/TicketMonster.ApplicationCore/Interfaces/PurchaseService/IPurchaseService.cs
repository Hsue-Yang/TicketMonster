using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.ApplicationCore.Interfaces.PurchaseService
{
    public interface IPurchaseService
    {
        Task<Venue> GetVenue(int venueId);
        SeatNum GetEventSeat(int eventId, string sectionName, string seatNum);
        Task<List<SeatSection>> GetTheaterSeat();

        Task<List<Performer>> GetPerfomer(int eventId);
        Task<Event> GetEvent(int eventId);


        Task<List<SeatSection>> GetSeatSection(int eventId);
        Task<List<SeatNum>> GetSeatNum(int eventId);
        Task<string> GetEventPic(int eventId);
        Task<string> GetPerformerPic(int eventId);


    }
}
