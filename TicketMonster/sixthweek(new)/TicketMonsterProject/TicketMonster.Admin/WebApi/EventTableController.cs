using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketMonster.Admin.Interface;

namespace TicketMonster.Admin.WebApi;


public class EventTableController : BaseApiController
{
    private readonly IEventRepo eventRepo;

    public EventTableController(IEventRepo eventRepo) => this.eventRepo = eventRepo;  

    [HttpGet]
    
    public async Task<IActionResult> GetAllEvents()
    {
        var result = await eventRepo.GetAllEvents();
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetEventsById(int EventId)
    {
        var result = await eventRepo.GetEventsById(EventId);
        return Ok(result);
    }

  

}
