using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections.Immutable;
using TicketMonster.ApplicationCore.Interfaces.EventService;
using TicketMonster.ApplicationCore.Interfaces.EventService.EventDto;
using TicketMonster.ApplicationCore.Services.Lineup;
using TicketMonster.Web.Interfaces;
using TicketMonster.Web.ViewModels.Lineup;

namespace TicketMonster.Web.Services.Event
{
    public class EventViewModelService : IEventViewModelService
    {
        private readonly IEventService _eventService;
        private readonly IRepository<PerformerPic> _picRepository;
        public EventViewModelService(IEventService eventService, IRepository<PerformerPic> picRepository)
        {
            _eventService = eventService;
            _picRepository = picRepository;
        }

        // 主頁 點選 Navitem 拿到 Event  
        public async Task<List<EventViewModel>> GetEventPageByCategoryId(int categoryId, int? subCategoryId)
        {
            var events = new List<ApplicationCore.Entities.Event>();

            if (subCategoryId.HasValue)
            {
                events = await _eventService.GetEventBySubCategory(subCategoryId);
            }
            else
            {
                events = await _eventService.GetEventsByCateGory(categoryId);
            }
            var viewModels = new List<EventViewModel>();

            var eventVenueIds = events.Select(e => e.VenueId).ToList();
            var eventSubCaIds = events.Select(e => e.SubCategoryId).ToList();
			var venues = await _eventService.GetVenue(eventVenueIds);
            var subNames = await _eventService.GetSubCategoryByEventID(eventSubCaIds);
			
			foreach (var eventItem in events)
            {
                var performers = await _eventService.GetPerformerByEventId(eventItem.Id);
                var subName = subNames.FirstOrDefault(subCa => subCa.Id == eventItem.SubCategoryId);
                var venue = venues.FirstOrDefault(v => v.Id == eventItem.VenueId);
                var vm = new EventViewModel()
                {
                    EventId = eventItem.Id,
                    EventDate = eventItem.EventDate,
                    EventName = eventItem.EventName,
                    EventPerformers = performers.Select(p => new PerformerViewModel
                    {
                        PerformerId = p.Id,
                        PerformerName = p.Name,
                        // 用 performerId 拿到 performerPic內的 performerId 對應
                        PerformerPicUrl = _picRepository.FirstOrDefault(pic => pic.PerfomerId == p.Id).Pic

                    }).ToList(),
                    Venuename = venue.VenueName,
                    VenueAddress = venue.Location,
                    VenueLatitude = venue.Latitude,
                    VenueLongitude = venue.Longitude,
                    SubCaName = subName.SubCategoryName
                };
                // 拿到場館的資料
                viewModels.Add(vm);
            }

            return viewModels;
        }


        // 拿 這個 Category內的子類別

        public async Task<List<SubCategoryViewModel>> GetSubCategoryByCategoryId(int categoryId)
        {
            var subCas = await _eventService.GetSubcategory(categoryId);

            var subCategoeyVM = new List<SubCategoryViewModel>();

            foreach (var subCa in subCas)
            {
                var vm = new SubCategoryViewModel()
                {
                    CaId = subCa.CatagoryId,
                    SubCaId = subCa.Id,
                    SubCaName = subCa.SubCategoryName
                };

                subCategoeyVM.Add(vm);
            }

            return subCategoeyVM;
        }

        // 組合成 Mix ViewModel
        public async Task<MixViewModel> GetMixVM(int categoryId, int? subCategoryId)
        {

            var vm = new MixViewModel
            {
                SubCategories = await GetSubCategoryByCategoryId(categoryId),
                Events = await GetEventPageByCategoryId(categoryId, subCategoryId)

            };
            return vm;
        }


        // 拿 搜尋框的東西  輸入查詢字串, 表演者 或 活動名稱包含的字, 回傳複數 event ,
        // 從 Event 拿 所有包含該字的 字串
        public async Task<EventViewModelBySearch> GetEventBySearch(int?  categoryId, string? input , DateTime startDate, DateTime endDate, int? sort, string? dates, int page = 1, int pageSize = 10)
        {

            var eventBySearch =  _eventService.GetEventBySearchString(input,page,pageSize,startDate,endDate, sort,categoryId);

            int totalEvents =await  _eventService.GetTotalRecordsBySearchString(input, startDate,endDate, categoryId);

			int totalPages = (int)Math.Ceiling(totalEvents / (double)pageSize);

            //var pageData = eventBySearch.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModels = new List<EventViewModel>();
            var searchVm = new EventViewModelBySearch
            {
                Events = new List<EventViewModel>(),
                CategoryId = categoryId,
                StartDate = startDate,
                EndDate = endDate,
                KeyWord = input,
                SortType = sort
            };

            if (eventBySearch.Count > 0)
            {
				var eventVenueIds = eventBySearch.Select(e => e.VenueId).ToList();
				var eventSubCaIds = eventBySearch.Select(e => e.SubCategoryId).ToList();
				
				var subNames = await _eventService.GetSubCategoryByEventID(eventSubCaIds);
				var venues = await _eventService.GetVenue(eventVenueIds);
				foreach (var eventItem in eventBySearch)
                {
                    var performers = await _eventService.GetPerformerByEventId(eventItem.Id);
                    var subName = subNames.FirstOrDefault(sub => sub.Id == eventItem.SubCategoryId);
					var venue = venues.FirstOrDefault(v => v.Id == eventItem.VenueId);
					var vm = new EventViewModel()
                    {
                        EventId = eventItem.Id,
                        EventDate = eventItem.EventDate,
                        EventName = eventItem.EventName,
                        EventPerformers = performers.Select(p => new PerformerViewModel
                        {
                            PerformerId = p.Id,
                            PerformerName = p.Name,
                            // 用 performerId 拿到 performerPic內的 performerId 對應
                            PerformerPicUrl = _picRepository.FirstOrDefault(pic => pic.PerfomerId == p.Id).Pic

                        }).ToList(),

                        // 拿到場館的資料
                        Venuename = venue.VenueName,
                        VenueAddress = venue.Location,
                        VenueLongitude = venue.Longitude,
                        VenueLatitude = venue.Latitude,
                        // 取該事件的子類別
                        SubCaName = subName.SubCategoryName
                    };
                    viewModels.Add(vm);
                };
                searchVm = new EventViewModelBySearch
                {
                    Events = viewModels,
                    CategoryId = categoryId,
                    StartDate = startDate,
                    EndDate = endDate,
                    KeyWord = input,
                    SortType = sort,
                    CurrentPage = page,
                    TotalPages = totalPages,
                    PageSize = pageSize,
                    Dates = dates
                };
            }
            return searchVm;

        }

        // 拿到 Nav裡面的 CategoryName   不需要
        //public async Task<List<CategoryViewModel>> GetCategoryName()
        //{
        //    var allCa = await _eventService.GetAllCategory();

        //    var categoryNamesVM = new List<CategoryViewModel>();

        //    foreach (var category in allCa)
        //    {
        //        var vm = new CategoryViewModel()
        //        {
        //            CaId = category.Id,
        //            CaName = category.CategoryName
        //        };

        //        categoryNamesVM.Add(vm);
        //    }

        //    return categoryNamesVM;
        //}


        // 主頁 點選 EventCard 拿到 Event
        //public async Task<MixViewModel> GetEventPageByEventID(int eventId)
        //{
        //    var events = await _eventService.GetEventsNameByEventId(eventId);

        //    var viewModels = new List<EventViewModel>();


        //    foreach (var eventItem in events)
        //    {
        //        var performers = await _eventService.GetPerformerByEventId(eventItem.Id);
        //        var vm = new EventViewModel()
        //        {
        //            EventId = eventItem.Id,
        //            EventDate = eventItem.EventDate,
        //            EventName = eventItem.EventName,
        //            EventPerformers = performers.Select(p => new PerformerViewModel
        //            {
        //                PerformerId = p.Id,
        //                PerformerName = p.Name,
        //                // 用 performerId 拿到 performerPic內的 performerId 對應
        //                PerformerPicUrl = _picRepository.FirstOrDefault(pic => pic.PerfomerId == p.Id).Pic

        //            }).ToList(),

        //        };

        //        // 拿到場館的資料
        //        var venue = await _eventService.GetVenue(eventItem.VenueId);
        //        vm.Venuename = venue.VenueName;
        //        vm.VenueAddress = venue.Location;
        //        vm.VenueLatitude = venue.Latitude;
        //        vm.VenueLongitude = venue.Longitude;
        //        // 取該事件的子類別
        //        var subName = await _eventService.GetSubCategoryByEventID(eventItem.SubCategoryId);
        //        vm.SubCaName = subName.SubCategoryName;

        //        viewModels.Add(vm);


        //    }
        //    // 將 Event 回傳至 MixViewModel
        //    var mixVM = new MixViewModel
        //    {
        //        Events = viewModels
        //    };
        //    return mixVM;
        //}
    }
}
