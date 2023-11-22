namespace TicketMonster.Web.ViewModels.Lineup
{
    public class EventViewModel
    {
        // model 本身就是一個 List, 表演者
        public int  EventId { get; set; }
        public string EventName { get; set; }
        public string Venuename { get; set; }
        public DateTime EventDate { get; set; }
        public string VenueAddress { get; set; }
        public List<PerformerViewModel> EventPerformers { get; set; }
        public string SubCaName { get; set; }
        public decimal VenueLatitude { get; set; }
        public decimal VenueLongitude { get; set; }
    }

    
    

}
