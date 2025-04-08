namespace TicketMonster.ApplicationCore.Services.User;

public partial class UserService
{
    public class ActiveEventsDTO
    {
        public int Id { get; set; }

        public string EventName { get; set; }

        public DateTime EventDate { get; set; }

        public string EventPic { get; set; }

        public string VenueLocation { get; set; }

        public int EventCount { get; set; }

        public int PerfomerID { get; set;}
    }
}
