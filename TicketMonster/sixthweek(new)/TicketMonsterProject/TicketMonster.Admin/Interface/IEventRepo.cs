using TicketMonster.Admin.Models.Create;

namespace TicketMonster.Admin.Interface;

public interface IEventRepo
{
    Task<IEnumerable<dynamic>> GetAllEvents();
    Task<int> CreateNewEvents(EventAndPicDto eventAndPicDto);
    Task<IEnumerable<dynamic>> GetCategoryNameByCategoryId(int CategoryId);
    Task<IEnumerable<dynamic>> GetEventsById(int EventId);
    Task<IEnumerable<dynamic>> GetAllCategoryName();
    Task<IEnumerable<dynamic>> GetAllSubCategoryNameAndCategoryId();   
    Task<IEnumerable<dynamic>> GetAllVenueName();
    Task<IEnumerable<dynamic>> GetAllPerformerName();
}
