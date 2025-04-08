using Microsoft.AspNetCore.Mvc;
using TicketMonster.ApplicationCore.Interfaces.User;

namespace TicketMonster.Web.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferTicketsController : ControllerBase
    {
        private readonly IUserService _userService;

        public TransferTicketsController(IUserService userService) => _userService = userService;

        [HttpPost]
        public async Task<IActionResult> TransferTickets(string email ,int id)
        {
            var result = await _userService.TransferTickets(email, id);
            return Ok(result);
        }
    }
}