using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces.EventService.EventDto;

namespace TicketMonster.ApplicationCore.Interfaces.EventService
{
    public interface IEventService
    {
        Task<List<Category>> GetAllCategory();
        
        // 搜尋 input 在資料庫內的活動
        List<Event> GetEventBySearchString(string input, int page, int pageSize, DateTime startDate, DateTime endDate, int? sort, int? caId);
        Task<List<Event>> GetEventBySubCategory(int? subCategoryId);

        // 舊
        //public Task<List<EventCardDto>> GetEventInfoAsync(int categoryId);


        // 新改
        // 方法: 從Event表內  傳入 CategoryId 取 多筆 EventId
        Task<List<Event>> GetEventsByCateGory(int categoryId);
        Task<List<Event>> GetEventsNameByEventId(int eventId);
        Task<List<Performer>> GetPerformerByEventId(int eventId);
        Task<List<SubCategory>> GetSubcategory(int categoryId);
        Task<List<SubCategory>> GetSubCategoryByEventID(List<int> subCategoryId);
		Task<int> GetTotalRecordsBySearchString(string input, DateTime startDate, DateTime endDate, int? caId);
		Task<List<Venue>> GetVenue(List<int> venueId);


        
    }


    
}
