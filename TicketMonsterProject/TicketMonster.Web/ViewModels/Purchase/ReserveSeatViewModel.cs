namespace TicketMonster.Web.ViewModels.Purchase
{
    public class ReserveSeatViewModel
    {
        public int EventId { get; set; }
        public SeatNum SeatNum { get; set; }
        public SeatSection SectionName { get; set; }
        public string ticketName { get; set; }
    }
}
