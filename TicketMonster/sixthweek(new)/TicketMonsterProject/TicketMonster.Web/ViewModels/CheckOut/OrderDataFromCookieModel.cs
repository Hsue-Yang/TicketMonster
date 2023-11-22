namespace TicketMonster.Web.ViewModels.CheckOut
{
    public class OrderDataFromCookieModel //從Cookie傳進來的
    {      
        public int EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
        public int TicketCount { get; set; }
        public decimal TicketPrice { get; set; }
        public string TicketName { get; set; }
        public int BillingAmount { get; set; }
    }
}
