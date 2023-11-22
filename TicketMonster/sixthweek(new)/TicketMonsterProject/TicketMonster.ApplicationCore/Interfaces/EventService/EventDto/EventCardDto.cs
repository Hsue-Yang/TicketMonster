using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketMonster.ApplicationCore.Interfaces.EventService.EventDto
{
    public class EventCardDto
    {
        public int CategoryId { get; set; }
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string VenueName { get; set; }
        public int VenieId { get; set; }
        public DateTime EventTime { get; set; }

        public List<Perform> Performs { get; set; }

    }
    public class Perform
    {
        public int PerformID { get; set; }
        public string PerformName { get; set; }


    }
    public class EventVenue
    {
        public int VenueID { get; set; }
        public string VenueName { get; set; }


    }
}
