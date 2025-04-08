using System.Diagnostics.Tracing;
using System.Linq;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Interfaces.PerformerService;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TicketMonster.ApplicationCore.Services
{
    public class PerformerService : IPerformerService
    {
        private readonly IRepository<Performer> _PerformerRepo;
        private readonly IRepository<PerformerPic> _PerformerPicRepo;
        private readonly IRepository<Event> _EventRepo;
        private readonly IRepository<EventPerform> _EventPerformRepo;
        private readonly IRepository<Category> _CategoryRepo;
        private readonly IRepository<Venue> _VenueRepo;
        private readonly IRepository<EventsPic> _EventPicsRepo;
        private readonly IRepository<SubCategory> _SubCategoryRepo;

        public PerformerService(
            IRepository<Performer> performerRepo,
            IRepository<PerformerPic> performerPicRepo,
            IRepository<Event> eventRepo,
            IRepository<EventPerform> eventperformRepo,
            IRepository<Category> categoryRepo,
            IRepository<Venue> venueRepo,
            IRepository<EventsPic> eventPicsRepo,
            IRepository<SubCategory> subcategoryRepo
        )
        {
            _PerformerRepo = performerRepo;
            _PerformerPicRepo = performerPicRepo;
            _EventRepo = eventRepo;
            _EventPerformRepo = eventperformRepo;
            _CategoryRepo = categoryRepo;
            _VenueRepo = venueRepo;
            _EventPicsRepo = eventPicsRepo;
            _SubCategoryRepo = subcategoryRepo;
        }


        public async Task<(
            List<Performer> PerformerList,
            List<PerformerPic> PerformerPic,
            List<Event> EventList,
            List<EventPerform> EventPerformList,
            List<Category> CategoryList,
            List<Venue> VenueList
        )> GetPerformerData()
        {            
                var performerList = await _PerformerRepo.ListAsync();
                var performerPic = await _PerformerPicRepo.ListAsync();
                var eventList = await _EventRepo.ListAsync();
                var eventCount = await _EventPerformRepo.ListAsync();
                var category = await _CategoryRepo.ListAsync();
                var venue = await _VenueRepo.ListAsync();
                return (performerList, performerPic, eventList, eventCount, category, venue);                      
        }      

        public async Task<Performer> GetPerformerById(int performerid)
        {
            var performer = await _PerformerRepo.GetByIdAsync(performerid);
            return performer;
        }

        public async Task<List<Event>> GetEventByPerformerId(int performerid)
        {       
            var performer = await _EventPerformRepo.ListAsync(x => x.PerfomerId == performerid);
            if (performer != null )
            {
                var eventIds = performer.Select(x => x.EventId).ToList();
                var events = await _EventRepo.ListAsync(x => eventIds.Contains(x.Id));
                return events;
            }
            else
            {              
                return null; 
            }         
        }

        public async Task<PerformerPic> GetPerformerPicByPerformerId(int performerid)
        {
            var performerpic = await _PerformerPicRepo.FirstOrDefaultAsync(x => x.PerfomerId == performerid);
            return performerpic;
        }

        public async Task<PerformerPic> GetPerformerHorizenPicByPerformerId(int performerid)
        {
            var performerpics = await _PerformerPicRepo.ListAsync(x => x.PerfomerId == performerid);
            var horizenperformerpic = performerpics.FirstOrDefault(x => x.Sort == 2);
            return horizenperformerpic;
        }


        public async Task<string> GetPerformerAboutByPerformerId(int performerid)
        {
            var performer = await _PerformerRepo.GetByIdAsync(performerid);
            var aboutperformer = performer.About;
            return aboutperformer;
        }

        public async Task<(Category performerCategory, SubCategory performerSubCategory)> GetPerformerCategory(int performerId)
        {
            var performer = await _PerformerRepo.GetByIdAsync(performerId);
            var performerCategory = await _CategoryRepo.GetByIdAsync(performer.CategoryId);
            var categoryName = performerCategory.CategoryName;
            var performerSubCategory = await _SubCategoryRepo.GetByIdAsync(performer.SubCategoryId);
            var subcategoryName = performerSubCategory.SubCategoryName;
            return (performerCategory, performerSubCategory);
        }

        public async Task<int> GetPerformerCategoryId(int performerId)
        {
            var performer = await _PerformerRepo.GetByIdAsync(performerId);
            var performerCategory = await _CategoryRepo.GetByIdAsync(performer.CategoryId);
            var id = performerCategory.Id;
            return id;
        }

        public async Task<List<Event>> GetEventByPerformerIdSortByTime(int performerid, DateTime startTime, DateTime endTime)
        {
            var performer = await _EventPerformRepo.ListAsync(x => x.PerfomerId == performerid);
            if (performer != null)
            {
                var eventIds = performer.Select(x => x.EventId).ToList();
                var events = await _EventRepo.ListAsync(x => eventIds.Contains(x.Id));
                var eventsDate = await _EventRepo.ListAsync(x => eventIds.Contains(x.Id) && x.EventDate >= startTime && x.EventDate <= endTime);
                var sortedEvents = eventsDate.OrderBy(x => x.EventDate).ToList();
                return sortedEvents;
            }
            else
            {
                return null;
            }
        }
    }
}
