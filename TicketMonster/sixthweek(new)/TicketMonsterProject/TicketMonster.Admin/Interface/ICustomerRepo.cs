namespace TicketMonster.Admin.Interface;

public interface ICustomerRepo
{
    Task<IEnumerable<dynamic>> GetAllCustomers();
    Task<IEnumerable<dynamic>> GetTargetOrders(int customerId);
    Task<IEnumerable<dynamic>> GetTargetOrderdetails(int orderId);
}
