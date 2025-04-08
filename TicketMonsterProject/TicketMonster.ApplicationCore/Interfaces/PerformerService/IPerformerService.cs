using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.ApplicationCore.Interfaces.PerformerService
{
    public interface IPerformerService
    {
        Task<(
            List<Performer> PerformerList,
            List<PerformerPic> PerformerPic,
            List<Event> EventList,
            List<EventPerform> EventPerformList,
            List<Category> CategoryList,
            List<Venue> VenueList
        )> GetPerformerData();     

        Task<Performer> GetPerformerById(int performerid);
        Task<List<Event>> GetEventByPerformerId(int performerid);
        Task<PerformerPic> GetPerformerPicByPerformerId(int performerid);
        Task<string> GetPerformerAboutByPerformerId(int performerid);
        Task<(Category performerCategory, SubCategory performerSubCategory)> GetPerformerCategory(int performerId);
        Task<PerformerPic> GetPerformerHorizenPicByPerformerId(int performerid);
        Task<int> GetPerformerCategoryId(int performerId);

    }
}
