using Azure;
using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Interfaces.EventService;
using TicketMonster.ApplicationCore.Interfaces.EventService.EventDto;

namespace TicketMonster.ApplicationCore.Services.Lineup
{
	public class EventService : IEventService
	{
		private readonly IRepository<Performer> _PerformerRepo;
		private readonly IRepository<PerformerPic> _PerformerPicRepo;
		private readonly IRepository<Event> _EventRepo;
		private readonly IRepository<EventsPic> _EventsPicRepo;

		private readonly IRepository<EventPerform> _EventPerformRepo;
		private readonly IRepository<Category> _CategoryRepo;
		private readonly IRepository<Venue> _VenueRepo;
		private readonly IRepository<SubCategory> _SubCategory;

		public EventService(IRepository<Performer> performerRepo, IRepository<Event> eventRepo, IRepository<EventPerform> eventPerformRepo, IRepository<Category> categoryRepo, IRepository<PerformerPic> performerPicRepo, IRepository<Venue> venueRepo, IRepository<SubCategory> subCategory, IRepository<EventsPic> eventPicRepo)
		{
			_PerformerRepo = performerRepo;
			_EventRepo = eventRepo;
			_EventPerformRepo = eventPerformRepo;
			_CategoryRepo = categoryRepo;
			_PerformerPicRepo = performerPicRepo;
			_VenueRepo = venueRepo;
			_SubCategory = subCategory;
			_EventsPicRepo = eventPicRepo;
		}

		// 傳入 CategoryId, 看有幾筆 Event
		public async Task<List<Event>> GetEventsByCateGory(int categoryId)
		{
			var events = await _EventRepo.ListAsync(x => x.CategoryId == categoryId && x.EventDate > DateTime.Now);
			var eventsOrder = events.OrderBy(x => x.EventDate).ToList();
			return eventsOrder;
		}

		// 傳入 SubCategoryId, 取資料
		public async Task<List<Event>> GetEventBySubCategory(int? subCategoryId)
		{
			var events = await _EventRepo.ListAsync(x => x.SubCategoryId == subCategoryId && x.EventDate > DateTime.Now);
			var eventsOrder = events.OrderBy(x => x.EventDate).ToList();
			return eventsOrder;
		}


		// 傳入 EventId , 拿到 同名的 Event 資料

		public async Task<List<Event>> GetEventsNameByEventId(int eventId)
		{
			//  傳入 EventId 拿到單筆的 Event資料
			var singleEvent = await _EventRepo.GetByIdAsync(eventId);

			// 取得這筆 Event 的名字
			var eventName = singleEvent.EventName;

			// 找到其他同名的 Event
			var sameNameEvents = await _EventRepo.ListAsync(e => e.EventName == eventName);

			var sameNameEventsOrder = sameNameEvents.OrderBy(x => x.EventDate).ToList();

			return sameNameEventsOrder;
		}

		// 傳入 VenueId, 看會是在哪個場館
		public async Task<List<Venue>> GetVenue(List<int> venueIds)
		{
			var venue = await _VenueRepo.ListAsync(v=> venueIds.Contains(v.Id));

			return venue;
		}
		// 傳入 PerformerId, 看圖


		public async Task<List<Performer>> GetPerformerByEventId(int eventId)
		{
			//var eventPerform = await _EventPerformRepo.ListAsync(x => x.EventId == eventId);

			// 傳入 EventId 在 EventPerform表內 找到多筆 EventId 相符的

			//var eventPerform = await _EventPerformRepo.ListAsync(x => x.EventId == eventId);
			var eventPerform =  _EventPerformRepo.Where(x => x.EventId == eventId);



			// 從 eventPerform內 拿到 多筆 包含傳入EventPerform內的 EventId, 選出 eventPerform內包含 Performer內的performerId
			var performers = await _PerformerRepo.ListAsync(p => eventPerform.Select(ep => ep.PerfomerId).Contains(p.Id));

			return performers;
		}

		// 取得所有 SubCa
		public async Task<List<SubCategory>> GetSubcategory(int categoryId)
		{
			var allSubCa = await _SubCategory.ListAsync(x => true);

			// 取得 相同 CategoryId 的 SubCategoryId的 Name 
			var subCaByCa = allSubCa.Where(subCa => subCa.CatagoryId == categoryId).ToList();

			return subCaByCa;
		}

		// 取得所有CategoryId
		public async Task<List<Category>> GetAllCategory()
		{
			var category = await _CategoryRepo.ListAsync(x => true);

			return category;
		}

		// 取得每筆 Event的 SubcategoryName
		public async Task<List<SubCategory>> GetSubCategoryByEventID(List<int> subCategoryIds)
		{
			var subCa = await _SubCategory.ListAsync(x => subCategoryIds.Contains(x.Id));


			return subCa;
		}

		// 看搜尋結果有幾筆
		public async Task<int> GetTotalRecordsBySearchString(string input, DateTime startDate, DateTime endDate, int? caId=0)
		{
			int totalRecord;

			if (!string.IsNullOrEmpty(input))
			{
				totalRecord = await _EventRepo.CountAsync(x =>(caId ==0 || x.CategoryId == caId) &&  x.EventName.Contains(input) && x.EventDate >= startDate && x.EventDate < endDate.AddDays(1));
			}
			else 
			{
				totalRecord = await _EventRepo.CountAsync(x => x.EventDate >= startDate && x.EventDate < endDate.AddDays(1) && (caId == 0 || x.CategoryId == caId));
			}
			
			

			return totalRecord;
		}

		// 搜尋框輸入字串,從資料庫拉資料
		public List<Event> GetEventBySearchString(string input,int page, int pageSize, DateTime startDate, DateTime endDate, int? sort,int? caId =0)
		{

			// 去事件表內 找 名稱包含輸入的關鍵字
			// 為空
			IQueryable<Event> query = _EventRepo.GetAllReadOnly().OrderBy(x=>x.EventDate);
			
			if (!string.IsNullOrEmpty(input) )
			{
				query = query.Where(x => (caId == 0 || x.CategoryId == caId)&& x.EventName.Contains(input) && x.EventDate >= startDate && x.EventDate < endDate.AddDays(1));
			}
			else
			{
				query = query.Where(x=> (caId == 0 || x.CategoryId == caId) && x.EventDate >= startDate && x.EventDate < endDate.AddDays(1));
			}




			if (sort == 1)
			{
				query = query.OrderByDescending(x => x.EventDate);
			}
			else if(sort == 2)
			{
				query = query.OrderBy(x => x.EventName);
			}
			else if(sort == 3)
			{
				query = query.OrderByDescending(x => x.EventName);
			}
				 
			var pageEvents = query.Skip((page -1) * pageSize).Take(pageSize).ToList();
			return pageEvents;
			
			////var query = await _EventRepo.ListAsync(x => x.EventDate > DateTime.Now);

			////if (!string.IsNullOrEmpty(input))
			////{
			////    query = query.Where(x => x.EventName.Contains(input,StringComparison.OrdinalIgnoreCase)).ToList();
			////}
			//////var events = await _EventRepo.ListAsync(x => x.EventName.Contains(input) & x.EventDate >DateTime.Now);
			////var eventsOrder = query.OrderBy(x => x.EventDate).ToList();
		}


		public async Task<List<EventsPic>> GetRandomEventPic(int count)
		{
			var expiredevent = await _EventRepo.ListAsync(x=>x.EventDate> DateTime.Now);
			var expiredeventid = expiredevent.Select(x => x.Id).ToList();
            var eventPerform = await _EventPerformRepo.ListAsync(x => expiredeventid.Contains(x.EventId));
            var eventpic = await _EventsPicRepo.ListAsync(p => eventPerform.Select(ep => ep.PerfomerId).Contains(p.Id));
			//var randomEventPic = eventpic.Where(x => x.Sort == 2).OrderBy(x => Guid.NewGuid()).Take(count).ToList();
			var randomEventPic = eventpic.OrderBy(x => Guid.NewGuid()).Take(count).ToList();
			return randomEventPic;
		}
	}
}
