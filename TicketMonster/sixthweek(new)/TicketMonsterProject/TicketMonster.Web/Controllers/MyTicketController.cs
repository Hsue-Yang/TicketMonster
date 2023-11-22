using Microsoft.AspNetCore.Mvc;

namespace TicketMonsterWebAppMVC.Controllers
{
    public class MyTicketController : Controller
    {
        public IActionResult MyTicketView()
        {
            return View();
        }
    }
}
