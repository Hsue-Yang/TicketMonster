

namespace TicketMonster.Web.ViewModels
{
    public class AccountPageViewModel
    {
        
        public int ID { get; set; }
        public string Pic { get; set; }
        public string EventName { get; set; }
        public string FirstName { get; set; }
        public string LasttName { get; set; }
        public DateTime SignDate { get; set; }

        public string Email { get; set; }
        public string Mobile { get; set; }

        public DateTime EventDate { get; set; }

        public string EventLocation { get; set; }
        public int TicketCount { get; set; }
    }
}
