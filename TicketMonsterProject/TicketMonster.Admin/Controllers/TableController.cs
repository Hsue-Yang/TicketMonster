using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketMonster.Admin.Controllers;

public class TableController : Controller
{
    public IActionResult UsersTable()
    {
        return View();
    }

    public IActionResult AllEventTable()
    {
        return View();
    }
}
