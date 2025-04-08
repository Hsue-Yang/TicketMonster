using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Interface;

namespace TicketMonster.Admin.WebApi;

public class CustomerTableController : BaseApiController
{
    private readonly ICustomerRepo customerRepo;

    public CustomerTableController(ICustomerRepo customerRepo) => this.customerRepo = customerRepo; 

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await customerRepo.GetAllCustomers();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetEventsByUser(int id)
    {
        var result = await customerRepo.GetTargetOrders(id);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetSeatsByUser(int id)
    {
        var result = await customerRepo.GetTargetOrderdetails(id);
        return Ok(result);
    }
}
