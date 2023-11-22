using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.ApplicationCore.Services
{
    public class CheckOutService
    {
        private readonly IRepository<Event> _eventRepo;
        private readonly IRepository<EventsPic> _eventpicRepo;
        private readonly IRepository<EventPerform> _eventperformRepo;
        private readonly IRepository<Venue> _venueRepo;     

        public CheckOutService(IRepository<EventsPic> eventpicRepo,IRepository<Event> eventRepo,IRepository<EventPerform> eventperformRepo,IRepository<Venue> venueRepo)
        {
            _eventpicRepo = eventpicRepo;
            _eventRepo = eventRepo; 
            _eventperformRepo = eventperformRepo;
            _venueRepo = venueRepo;       
        }

        public async Task<string> GetVenueNameByEventId(int eventid)
        {        
            var eventt = await _eventRepo.GetByIdAsync(eventid);
            var venueid = eventt.VenueId;
            var venues = await _venueRepo.GetByIdAsync(venueid);
            var venuename = venues.VenueName;
            return venuename;
        }
        
        public async Task<string> GetVenueLocationByEventId(int eventid)
        {
            var eventt = await _eventRepo.GetByIdAsync(eventid);
            var venueid = eventt.VenueId;
            var venues = await _venueRepo.GetByIdAsync(venueid);
            var venueLocation = venues.Location;
            return venueLocation;
        }


        public async Task<string> GetOneOfEventPicAsync(int eventId)
        {
            var eventPics = await _eventpicRepo.ListAsync(e => e.EventId == eventId);
            var eventpics = eventPics.FirstOrDefault(x => x.Sort == 1)?.Pic;
            return eventpics;      
        }

        public async Task<(decimal Latitude, decimal Longitude)> GetVenueCoordinatesByEventId(int eventid)
        {
            var eventt = await _eventRepo.GetByIdAsync(eventid);
            var venueid = eventt.VenueId;
            var venues = await _venueRepo.GetByIdAsync(venueid);
            return (venues.Latitude, venues.Longitude);
        }


        public async Task<List<string>> GetEventPicsAsync(int eventId)
        {
            var eventPics = await _eventpicRepo.ListAsync(e => e.EventId == eventId);
            var eventpics = eventPics.Select(x=>x.Pic).ToList();
            return eventpics;
        }

        public async Task<string> GetVenueSvgAsync(int eventId)
        {
            var eventt = await _eventRepo.FirstOrDefaultAsync(e => e.Id == eventId);
            var venuenid = eventt.VenueId;
            var venue = await _venueRepo.FirstOrDefaultAsync(v => v.Id == venuenid);
            var venuesvg = venue.Pic;
            return venuesvg;
        }
        
        public async Task<string> GetEventNameByEventId(int eventId)
        {
            var eventt = await _eventRepo.GetByIdAsync(eventId);
            var eventname = eventt.EventName;
            return eventname;
        }

        public async Task<DateTime> GetEventDateByEventId(int eventId)
        {
            var eventt = await _eventRepo.GetByIdAsync(eventId);
            var eventdate = eventt.EventDate;
            return eventdate;
        }        
     }
}
