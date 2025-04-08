using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Interfaces.User;

namespace TicketMonster.ApplicationCore.Services.User;

public partial class UserService : IUserService
{
    private readonly string _TicketMonsterConnection;
    private readonly IRepository<Customer> _customerRepo;
    private readonly IRepository<Order> _OrderRepo;
    private readonly IRepository<OrderDetail> _OrderDetailRepo;

    public UserService(IConfiguration configuration, IRepository<Customer> customerRepo, IRepository<Order> orderRepo, IRepository<OrderDetail> orderDetailRepo)
    {
        _customerRepo = customerRepo;
        _OrderRepo = orderRepo;
        _OrderDetailRepo = orderDetailRepo;
        _TicketMonsterConnection = configuration.GetSection("ConnectionStrings:TicketMonsterConnection").Value;
    }

    public async Task<Customer> GetCustomerInfo(int customerId)
    {
        return await _customerRepo.GetByIdAsync(customerId);
    }

    public async Task UpdateCustomerInfo(Customer input)
    {
        await _customerRepo.UpdateAsync(input);
    }

    public async Task RenewPasssword(Customer passsword)
    {
        await _customerRepo.UpdateAsync(passsword);
    }

    public async Task<Order> GetNextEventInfo(int customerId)
    {
        var customer = await _customerRepo.GetByIdAsync(customerId);
        var order = await _OrderRepo.OrderByAsync(x => x.EventDate);
        var result = order.FirstOrDefault(x => x.CustomerId == customer.Id && x.EventDate > DateTime.Now);
        return result;
    }

    public async Task<List<Order>> GetOrderEventsInfo(int customerId)
    {
        var customer = await _customerRepo.GetByIdAsync(customerId);
        var orders = await _OrderRepo.ListAsync(x => x.CustomerId == customer.Id);
        return orders;
    }

    public async Task<Order> GetCurrentEvent(int orderId)
    {
        var order = await _OrderRepo.GetByIdAsync(orderId);
        return order;
    }

    public async Task<List<OrderDetail>> GetCurrentEventDetails(int orderId)
    {
        var order = await _OrderRepo.GetByIdAsync(orderId);
        var orderDetails = await _OrderDetailRepo.ListAsync(x => x.OrderId == order.Id);
        return orderDetails;
    }

    public async Task<int> TransferTickets(string email, int id)
    {
        using var conn = new SqlConnection(_TicketMonsterConnection);
        await conn.OpenAsync();
        string sql = "UPDATE orders SET customerId = (select c.id from customers c where c.email = @email) WHERE id = @id";
        var parameters = new { email, id };
        var affectedRows = await conn.ExecuteAsync(sql, parameters);
        await conn.CloseAsync();
        return affectedRows;
    }

    public async Task<IEnumerable<ActiveEventsDTO>> GetActiveEvents()
    {
        var conn = new SqlConnection(_TicketMonsterConnection);
        await conn.OpenAsync();
        var query = @"select e.ID, e.EventName, e.EventDate, v.Location as VenueLocation, ep.Pic as EventPic, pf.PerfomerID, COUNT(*) OVER (partition by e.EventName) as EventCount
                      from Events e INNER JOIN Venues v ON e.VenueID = v.ID INNER JOIN EventsPic ep ON e.ID = ep.EventID INNER JOIN EventPerform pf ON e.ID = pf.EventID
                      where ep.Sort = 1 AND EventDate > SYSDATETIME() order by NEWID()";
        var result = conn.QueryAsync<ActiveEventsDTO>(query).Result;
        await conn.CloseAsync();
        return result;
    }
}