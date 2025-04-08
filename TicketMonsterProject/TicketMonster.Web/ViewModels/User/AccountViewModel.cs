using static TicketMonster.ApplicationCore.Services.User.UserService;

namespace TicketMonster.Web.ViewModels.User;

public class AccountViewModel
{
    public Customer Customer { get; set; }
    public Order Order { get; set; }
    public IEnumerable<ActiveEventsDTO> Events { get; set; }
}