using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.ApplicationCore.Interfaces.Access;

public interface IAccess
{
    bool IsExistUser(Customer customer);
    bool CanSignIn(Customer customer);
    Task SignUp(Customer customer);
    Task<Customer> GetUser(Customer customer);
    Task<Customer> GetUserByEmail(Customer customer);
}
