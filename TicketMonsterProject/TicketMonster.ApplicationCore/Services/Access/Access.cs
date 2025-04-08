using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Extensions;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Interfaces.Access;

namespace TicketMonster.ApplicationCore.Services.Access;

public class Access : IAccess
{
    private readonly IRepository<Customer> _customer;

    public Access(IRepository<Customer> customer) =>　_customer = customer;

    public bool IsExistUser(Customer customer)
    {
        return _customer.Any(c => c.Email == customer.Email);
    }

    public bool CanSignIn(Customer customer)
    {
        return _customer.Any(c => c.Email == customer.Email && c.Password == (customer.Password).ToSHA256());
    }

    public async Task SignUp(Customer input)
    {
        await _customer.AddAsync(input);
    }

    public async Task<Customer> GetUser(Customer customer)
    {
        return await _customer.SingleOrDefaultAsync(c => c.Email == customer.Email && c.Password == (customer.Password).ToSHA256());
    }

    public async Task<Customer> GetUserByEmail(Customer customer)
    {
        return await _customer.SingleOrDefaultAsync(c => c.Email == customer.Email);
    }
}
