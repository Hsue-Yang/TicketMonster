using Azure.Messaging;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json.Serialization;
using System.Linq;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Interfaces.PurchaseService;


namespace TicketMonster.ApplicationCore.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IRepository<Venue> _venueRepo;
        private readonly IRepository<EventsPic> _eventPicRepo;
        private readonly IRepository<PerformerPic> _performerPicRepo;
        private readonly IRepository<Performer> _performerRepo;
        private readonly IRepository<EventPerform> _eventPerform;
        private readonly IRepository<SeatSection> _seatSectionRepo;
        private readonly IRepository<SeatNum> _seatNumRepo;
        private readonly IRepository<EventPerform> _eventPerformRepo;



        public PurchaseService(IRepository<Event> eventRepo, IRepository<Venue> venueRepo, IRepository<Performer> performerRepo, IRepository<EventPerform> eventPerform, IRepository<SeatNum> seatNumRepo, IRepository<SeatSection> seatSectionRepo, IRepository<EventsPic> eventPicRepo, IRepository<PerformerPic> performerPicRepo, IRepository<EventPerform> eventPerformRepo)
        {
            _eventRepo = eventRepo;
            _venueRepo = venueRepo;
            _performerRepo = performerRepo;
            _eventPerform = eventPerform;
            _seatNumRepo = seatNumRepo;
            _seatSectionRepo = seatSectionRepo;
            _eventPicRepo = eventPicRepo;
            _performerPicRepo = performerPicRepo;
            _eventPerformRepo = eventPerformRepo;
        }

        public async Task<string> GetEventPic(int eventId)
        {
            try
            {
                var thispic = await _eventPicRepo.FirstOrDefaultAsync(ep => ep.EventId == eventId);
                var pic = thispic.Pic;
                return pic;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<string> GetPerformerPic(int eventId)
        {
            //先找到一筆活動
            try
            {
                var thisEvent = await GetEvent(eventId);
                //拿那一筆活動去找performer在eventPerform的PerfomerId
                var perfomer = _eventPerformRepo.FirstOrDefault(ep => thisEvent.Id == ep.EventId);
                var perfomerId = perfomer?.PerfomerId;
                //再拿perfomerId去比對perfomerPic的PerformerId
                var perfomerPic = await _performerPicRepo.FirstOrDefaultAsync(pp => perfomerId == pp.PerfomerId);
                var perPic = perfomerPic.Pic != null ? perfomerPic.Pic : _eventPicRepo.Where(ep => thisEvent.Id == ep.EventId).Select(ep => ep.Pic).ToString();

                return perPic;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<Venue> GetVenue(int eventId)
        {
            //1.拿到一個活動
            var thisEvent = await GetEvent(eventId);
            //2.拿到活動對應的場館
            var thisVenue = await _venueRepo.FirstOrDefaultAsync(v => v.Id == thisEvent.VenueId);
            return thisVenue;
        }
        public async Task<List<Performer>> GetPerfomer(int eventId)
        {
            //var thisEvent = await _eventRepo.GetByIdAsync(eventId);
            var thisEvent = await GetEvent(eventId);
            var thisPerform = await _eventPerform.ListAsync(x => x.EventId == thisEvent.Id);
            var performer = await _performerRepo.ListAsync(c => thisPerform.Select(p => p.PerfomerId).Contains(c.Id));
            //List<Performer> performers = new List<Performer>();
            //var perfomer = _performerRepo.ListAsync(p => p.Id == thisPerform);
            //thisPerform.ForEach(async x => performers.AddRange(await _performerRepo.ListAsync(c => c.Id == x.PerfomerId)));
            // 每場活動的演出者
            //foreach (var x in thisPerform)
            //{
            //    //表演者個資
            //    var y = await _performerRepo.ListAsync(c => c.Id == x.PerfomerId);
            //    performers.AddRange(y);
            //    // 迭代表演者個資
            //}
            return performer;
        }
        public async Task<Event> GetEvent(int eventId)
        {
            var thisEvent = await _eventRepo.GetByIdAsync(eventId);
            return thisEvent;
        }

        public async Task<List<SeatSection>> GetTheaterSeat()
        {
            //要拿到IsOrdered==true的座位資料
            //條件有 SectionName.Contains("Theater")
            //SeatSectionId==SeatSection.Id
            //這樣寫有問題
            var isOrderedSeat = _seatNumRepo.Where(sn => sn.IsOrdered == true).Select(sn => sn.SeatSectionId);
            var theaterSeat = await _seatSectionRepo.ListAsync(ss => isOrderedSeat.Contains(ss.Id) && ss.SectionName.Contains("Theater"));
            return theaterSeat;
        }
        public SeatNum GetEventSeat(int eventId, string sectionName, string seatNum)
        {
            try
            {
                //1.拿到一個活動
                //var thisEvent =  _eventRepo.FirstOrDefault(e=>e.Id == eventId);
                var thisEvent = GetEvent(eventId);
                //2.拿到對應活動的所有座位 sectionName跟seatSectionId匹配
                //3.用sectionName找到sectionId
                var sectioList = _seatSectionRepo.FirstOrDefault(es => es.EventId == thisEvent.Id && es.SectionName == sectionName); //發生例外
                var sectionId = sectioList.Id;
                //4.sectionId配合SeatNum找到唯一那一筆座位資訊
                var seatNums = _seatNumRepo.FirstOrDefault(sn => sn.SeatSectionId == sectionId && sn.SeatNum1 == seatNum);
                //5.取得那筆座位的獨立ID
                var seatid = seatNums.Id;
                var seat = _seatNumRepo.GetById(seatid);
                if (seat != null)
                {
                    //改這筆座位的訂位資訊
                    seat.IsOrdered = true;
                    seat.RetainTime = DateTime.Now;
                    //disposed again
                    return _seatNumRepo.Update(seat);
                }

                return seatNums;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<List<SeatSection>> GetSeatSection(int eventId)
        {
            //eventID 找到 VenueId
            var findevent = await GetEvent(eventId);
            var venueId = findevent.VenueId;
            //VenueId搭配eventId 找到SeatSection
            var seatSection = await _seatSectionRepo.ListAsync(ss => ss.VenueId == venueId && ss.EventId == eventId);
            return seatSection;
        }

        public async Task<List<SeatNum>> GetSeatNum(int eventId)
        {
            var seatSection = await GetSeatSection(eventId);
            var seatSeciotnId = seatSection.Select(ss => ss.Id);
            var seatNum = await _seatNumRepo.ListAsync(sn => seatSeciotnId.Contains(sn.SeatSectionId));
            return seatNum;
        }


    }
}
