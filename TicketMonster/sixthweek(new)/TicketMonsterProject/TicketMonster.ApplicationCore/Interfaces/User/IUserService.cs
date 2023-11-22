using TicketMonster.ApplicationCore.Entities;
using static TicketMonster.ApplicationCore.Services.User.UserService;

namespace TicketMonster.ApplicationCore.Interfaces.User;

public interface IUserService
{
    Task<Customer> GetCustomerInfo(int customerId);
    Task UpdateCustomerInfo(Customer customer);
    Task RenewPasssword(Customer passsword);
    Task<Order> GetNextEventInfo(int customerId);
    Task<List<Order>> GetOrderEventsInfo(int customerId);
    Task<Order> GetCurrentEvent(int customerId);
    Task<List<OrderDetail>> GetCurrentEventDetails(int orderId);
    Task<int> TransferTickets(string email, int id);
    Task<IEnumerable<ActiveEventsDTO>> GetActiveEvents();
}
