namespace TicketMonster.Web.ViewModels.User
{
    public class OrderDetailsViewModel
    {
        public Customer Customer { get; set; }
        public Order Order { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}