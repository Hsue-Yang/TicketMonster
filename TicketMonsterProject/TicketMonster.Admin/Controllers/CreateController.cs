using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TicketMonster.Admin.Controllers
{
     public class CreateController : Controller
    {       
        public IActionResult AddEvent()
        {
            return View();
        }
    }
} 