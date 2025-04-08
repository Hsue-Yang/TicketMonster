using TicketMonster.Admin.Interface;

namespace TicketMonster.Admin.Repository;

public class CustomerRepo : BaseRepo, ICustomerRepo
{
    public CustomerRepo(IConfiguration configuration) : base(configuration) { }

    public async Task<IEnumerable<dynamic>> GetAllCustomers()
    {
        var sql = "select * from customers";
        return await QueryAsync<dynamic>(sql);
    }

    public async Task<IEnumerable<dynamic>> GetTargetOrders(int customerId)
    {
        var sql = $"select o.* from Customers c inner join Orders o on c.ID = o.CustomerID where c.ID = @customerId";
        var parameter = new { customerId };
        return await QueryAsync<dynamic>(sql, parameter);
    }

    public async Task<IEnumerable<dynamic>> GetTargetOrderdetails(int orderId)
    {
        var sql = $"SELECT od.* FROM Customers c INNER JOIN Orders o ON c.ID = o.CustomerID INNER JOIN OrderDetails od ON o.ID = od.OrderID WHERE OrderID = @orderId";
        var parameter = new { orderId };
        return await QueryAsync<dynamic>(sql, parameter);
    }
}