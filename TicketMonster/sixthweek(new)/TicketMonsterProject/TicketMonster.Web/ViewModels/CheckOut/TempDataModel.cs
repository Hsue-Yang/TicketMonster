namespace TicketMonster.Web.ViewModels.CheckOut
{
    public class TempDataModel
    {
        public int CustomerId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public string EventPic { get; set; }
        public string VenueName { get; set; }
        public string VenueLocation { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}
